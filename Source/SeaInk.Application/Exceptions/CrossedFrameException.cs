using System;
using System.Runtime.Serialization;
using Kysect.Centum.Sheets.Indices;
using SeaInk.Application.TableLayout.Models;
using SeaInk.Core.Exceptions;

namespace SeaInk.Application.Exceptions
{
    [Serializable]
    public class CrossedFrameException : SeaInkException
    {
        public CrossedFrameException(ISheetIndex begin, ISheetIndex current, Frame frame)
            : base($"Index {current} should be in frame {frame} starting at {begin}") { }

        protected CrossedFrameException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}