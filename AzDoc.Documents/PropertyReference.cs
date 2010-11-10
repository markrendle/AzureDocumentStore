using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzDoc.Documents
{
    public class PropertyReference
    {
        private readonly string _path;
        private readonly string _propertyName;
        private readonly string _fullPath;

        public PropertyReference(string fullPath)
        {
            _fullPath = fullPath;

            if (fullPath.Contains('/'))
            {
                var bits = fullPath.Split('/');
                _propertyName = bits[bits.Length - 1];
                _path = string.Join("/", bits.Take(bits.Length - 1));
            }
            else
            {
                _path = string.Empty;
                _propertyName = fullPath;
            }
        }

        public PropertyReference(string path, string propertyName)
        {
            _path = path;
            _propertyName = propertyName;
        }

        public string FullPath
        {
            get { return _fullPath; }
        }

        public string Path
        {
            get { return _path; }
        }

        public string PropertyName
        {
            get { return _propertyName; }
        }
    }
}
