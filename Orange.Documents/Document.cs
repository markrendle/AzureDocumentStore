using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Orange.Json;

namespace Orange.Documents
{
    public class Document
    {
        private static readonly Func<string, object> NewDocument = s => new Document();
        private static readonly Func<string, object> NewList = s => new List<object>();
        private static readonly Func<string, object> NewDocumentList = s => new List<Document>();

        private readonly ConcurrentDictionary<string, object> _data;

        public Document()
        {
            _data = new ConcurrentDictionary<string, object>();
        }

        private Document(IEnumerable<KeyValuePair<string, object>> source)
            : this()
        {
            foreach (var pair in source)
            {
                var dict = pair.Value as IEnumerable<KeyValuePair<string, object>>;
                if (dict != null)
                {
                    SetSubDocument(pair.Key, new Document(dict));
                }
                else
                {
                    var list = pair.Value as IList<object>;
                    if (list != null)
                    {
                        var listOfDict = list.OfType<IEnumerable<KeyValuePair<string, object>>>();
                        if (listOfDict.Any())
                        {
                            SetDocumentList(pair.Key, listOfDict.Select(d => new Document(d)).ToList());
                        }
                        else
                        {
                            SetList(pair.Key, list);
                        }
                    }
                    else
                    {
                        SetAttribute(pair.Key, pair.Value);
                    }
                }
            }
        }

        public static Document Load(Stream stream)
        {
            var dictionary = JsonParser.Parse(stream);
            return new Document(dictionary);
        }

        public static Document Parse(string source)
        {
            var dictionary = JsonParser.Parse(source);
            return new Document(dictionary);
        }

        protected internal ConcurrentDictionary<string, object> Data
        {
            get { return _data; }
        }

        public object GetAttribute(string name)
        {
            try
            {
                return Data[name];
            }
            catch (KeyNotFoundException)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public bool TryGetAttribute(string name, out object value)
        {
            return Data.TryGetValue(name, out value);
        }

        public void SetAttribute(string name, object value)
        {
            Data[name] = value;
        }

        public Document GetSubDocument(string name)
        {
            return (Document)Data.GetOrAdd(name, NewDocument);
        }

        public void SetSubDocument(string name, Document document)
        {
            Data[name] = document;
        }

        public IList<object> GetList(string name)
        {
            return (IList<object>)Data.GetOrAdd(name, NewList);
        }

        public void SetList(string name, IList<object> list)
        {
            Data[name] = list;
        }

        public IList<Document> GetDocumentList(string name)
        {
            return (IList<Document>)Data.GetOrAdd(name, NewDocumentList);
        }

        public void SetDocumentList(string name, IList<Document> list)
        {
            Data[name] = list;
        }

        public IEnumerable<string> GetAttributeNames()
        {
            return
                Data.Where(kvp => !kvp.Value.GetType().In(typeof(Document), typeof(List<object>), typeof(List<Document>))).
                    Select(kvp => kvp.Key);
        }

        public IEnumerable<string> GetSubDocumentNames()
        {
            return Data.Where(kvp => kvp.Value.GetType() == typeof(Document)).
                    Select(kvp => kvp.Key);
        }

        internal IEnumerable<string> GetListNames()
        {
            return Data.Where(kvp => kvp.Value.GetType() == typeof(List<object>)).
                    Select(kvp => kvp.Key);
        }

        public IEnumerable<string> GetDocumentListNames()
        {
            return Data.Where(kvp => kvp.Value.GetType() == typeof(List<Document>)).
                    Select(kvp => kvp.Key);
        }

        public IEnumerable<object> FindValues(string path)
        {
            var result = Enumerable.Empty<object>();
            return FindValues(path.Split('/'), result);
        }

        private IEnumerable<object> FindValues(IEnumerable<string> path)
        {
            return FindValues(path, Enumerable.Empty<object>());
        }

        private IEnumerable<object> FindValues(IEnumerable<string> path, IEnumerable<object> result)
        {
            var key = path.First();
            if (!Data.ContainsKey(key)) throw new IndexOutOfRangeException();
            var item = Data[key];
            var rest = path.Skip(1);

            if (rest.Any())
            {
                var subDocument = item as Document;
                if (subDocument != null) return subDocument.FindValues(path.Skip(1));

                var subDocumentList = item as List<Document>;
                if (subDocumentList != null) return subDocumentList.SelectMany(dl => dl.FindValues(path.Skip(1)));

                return result;
            }

            var list = item as List<object>;
            return list == null ? result.Append(item) : result.Concat(list);
        }
    }
}
