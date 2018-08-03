﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LiteDB.Tests
{
    public class TempFile : IDisposable
    {
        public string FileName { get; private set; }

        public TempFile()
        {
            var path = Path.GetTempPath();
            var name = "test-" + Guid.NewGuid().ToString("d").Substring(0, 5) + ".db";

            this.FileName = Path.Combine(path, name);
        }

        #region Dispose

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TempFile()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only
            }

            // check file integrity

            File.Delete(this.FileName);

            _disposed = true;
        }

        #endregion

        public long Size => new FileInfo(this.FileName).Length;

        public string ReadAsText() => File.ReadAllText(this.FileName);
    }
}