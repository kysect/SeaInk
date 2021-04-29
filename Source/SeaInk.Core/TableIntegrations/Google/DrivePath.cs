using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SeaInk.Core.TableIntegrations.Google
{
    public class DrivePath: IEnumerable<string>
    {
        private readonly IEnumerable<string> _path;
        
        public DrivePath(IEnumerable<string> path)
        {
            _path = path;
        }

        public override string ToString()
        {
            return string.Join("/", _path);
        }

        public DrivePath(string pathString)
        {
            _path = pathString.Split("/").ToList();
        }
        
        public IEnumerator<string> GetEnumerator()
        {
           return _path.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}