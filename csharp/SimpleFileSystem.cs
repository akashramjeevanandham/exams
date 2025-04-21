using System;
using System.Collections.Generic;
using System.IO;

namespace Exams.CSharp
{
    public class SimpleFileSystem
    {
        private readonly Dictionary<string, string> _files;

        public SimpleFileSystem()
        {
            _files = new Dictionary<string, string>();
        }

        public void CreateFile(string filename, string content)
        {
            if (_files.ContainsKey(filename))
            {
                throw new InvalidOperationException("File already exists: " + filename);
            }
            _files[filename] = content;
        }

        public string ReadFile(string filename)
        {
            if (!_files.ContainsKey(filename))
            {
                throw new FileNotFoundException("File not found: " + filename);
            }
            return _files[filename];
        }

        public void WriteFile(string filename, string content)
        {
            if (!_files.ContainsKey(filename))
            {
                throw new FileNotFoundException("File not found: " + filename);
            }
            _files[filename] = content;
        }

        public void DeleteFile(string filename)
        {
            if (!_files.ContainsKey(filename))
            {
                throw new FileNotFoundException("File not found: " + filename);
            }
            _files.Remove(filename);
        }

        public IEnumerable<string> ListFiles()
        {
            return _files.Keys;
        }
    }
}
