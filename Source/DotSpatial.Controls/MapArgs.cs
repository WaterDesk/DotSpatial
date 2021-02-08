// Copyright (c) DotSpatial Team. All rights reserved.
// Licensed under the MIT license. See License.txt file in the project root for full license information.

using System;
using System.Drawing;
using DotSpatial.Data;
using System.Collections.Generic;

namespace DotSpatial.Controls
{
    /// <summary>
    /// The map arguments.
    /// </summary>
    public class MapArgs : EventArgs, IProj
    {
        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MapArgs"/> class.
        /// </summary>
        /// <param name="bufferRectangle">The buffer rectangle.</param>
        /// <param name="bufferEnvelope">The buffer envelope.</param>
        public MapArgs(Rectangle bufferRectangle, Extent bufferEnvelope)
        {
            ImageRectangle = bufferRectangle;
            GeographicExtents = bufferEnvelope;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapArgs"/> class, where the device is also specified, overriding the default buffering behavior.
        /// </summary>
        /// <param name="bufferRectangle">The buffer rectangle.</param>
        /// <param name="bufferEnvelope">The buffer envelope.</param>
        /// <param name="g">The graphics object used for drawing.</param>
        public MapArgs(Rectangle bufferRectangle, Extent bufferEnvelope, Graphics g)
        {
            ImageRectangle = bufferRectangle;
            GeographicExtents = bufferEnvelope;
            _gpBB = g;
            _ptBuffer = new Dictionary<ulong, int>();
            _strBuffer = new Dictionary<ulong, int>();
            LabelFormat = new StringFormat();
            LabelFormat.Alignment = StringAlignment.Center;
            LabelFormat.LineAlignment = StringAlignment.Near;

            // 
        }

        #endregion

        #region Member

        /// <summary>
        /// Add pointer to buffer
        /// </summary>
        /// <param name="x">pos x</param>
        /// <param name="y">pos y</param>
        /// <returns>can draw point</returns>
        public bool AddPointPos(int x, int y)
        {
            x = x / 3;
            y = y / 3;
            ulong key = (((ulong)(uint)(x / 3)) << 32) | ((ulong)(uint)(y / 3));
            if (_ptBuffer.ContainsKey(key))
            {
                return false;
            }

            _ptBuffer.Add(key, 0);

            return true;
        }

        public bool AddPointLabel(int x, int y, int count)
        {
            List<ulong> list = new List<ulong>();
            for(uint i = 0; i < (uint)count; i ++)
            {
                ulong key = (((ulong)(uint)((x / 16) + i)) << 32) | ((ulong)(uint)(y / 20));
                if (_strBuffer.ContainsKey(key))
                    return false;
                list.Add(key);
            }
            for(int i = 0; i < list.Count; i ++)
            {
                _strBuffer[list[i]] = 0;
            }
            return true;
        }

        public bool AddStringPos(int x, int y)
        {
            ulong key = (((ulong)(uint)(x / 16)) << 32) | ((ulong)(uint)(y / 20));
            if (_strBuffer.ContainsKey(key))
            {
                return false;
            }

            _strBuffer.Add(key, 0);

            return true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an optional parameter that specifies a device to use instead of the normal buffers.
        /// </summary>
        public Graphics Device { get{ return _gpBB;}}

        /// <summary>
        /// Gets the Dx
        /// </summary>
        public double Dx => GeographicExtents.Width != 0.0 ? ImageRectangle.Width / GeographicExtents.Width : 0.0;

        /// <summary>
        /// Gets the Dy
        /// </summary>
        public double Dy => GeographicExtents.Height != 0.0 ? ImageRectangle.Height / GeographicExtents.Height : 0.0;

        /// <summary>
        /// Gets the geographic bounds of the content of the buffer.
        /// </summary>
        public Extent GeographicExtents { get; }

        /// <summary>
        /// Gets the rectangle dimensions of what the buffer should be in pixels
        /// </summary>
        public Rectangle ImageRectangle { get; }

        /// <summary>
        /// Gets the maximum Y value
        /// </summary>
        public double MaxY => GeographicExtents.MaxY;

        /// <summary>
        /// Gets the minimum X value
        /// </summary>
        public double MinX => GeographicExtents.MinX;

        public Graphics gpBB { get{ return _gpBB;} set{ _gpBB = value;} }

        public Graphics gpBF { get; set;}

        public Font fontLabel {
            get{
                if(_font == null)
                    _font = new Font("ËÎÌו", 16);
                return _font;
            }
        }

        public StringFormat LabelFormat{ get; set;}

        private Font _font;

        private Dictionary<ulong, int> _ptBuffer;
        private Dictionary<ulong, int> _strBuffer;

        private Graphics _gpBB;



        #endregion

        
    }
}