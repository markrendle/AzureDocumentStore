using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzDoc.Documents
{
    public class PropertyReference : IEquatable<PropertyReference>
    {
        private readonly string _path;
        private readonly string _propertyName;
        private readonly string _fullPath;

        public PropertyReference(string fullPath)
        {
            if (fullPath == null) throw new ArgumentNullException("fullPath");
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
            if (path == null) throw new ArgumentNullException("path");
            if (propertyName == null) throw new ArgumentNullException("propertyName");
            _fullPath = string.IsNullOrWhiteSpace(path) ? propertyName : path + "/" + propertyName;
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

        public bool Equals(PropertyReference other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._path, _path) && Equals(other._propertyName, _propertyName) && Equals(other._fullPath, _fullPath);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PropertyReference)) return false;
            return Equals((PropertyReference) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = _path.GetHashCode();
                result = (result*397) ^ _propertyName.GetHashCode();
                result = (result*397) ^ _fullPath.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(PropertyReference left, PropertyReference right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PropertyReference left, PropertyReference right)
        {
            return !Equals(left, right);
        }
    }
}
