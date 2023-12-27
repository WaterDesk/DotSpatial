// Copyright (c) DotSpatial Team. All rights reserved.
// Licensed under the MIT license. See License.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using DotSpatial.Data;
using DotSpatial.Serialization;
using GeoAPI.Geometries;

namespace DotSpatial.Symbology
{
    /// <summary>
    /// event assign fast drawn states class
    /// </summary>
    public class AssignFastDrawnStatesEvent : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignFastDrawnStatesEvent"/> class.
        /// </summary>
        /// <param name="layer">The FeatureLayer of the event.</param>
        /// <param name="scheme">The IFeatureScheme of the event.</param>
        /// <param name="dataSet">The IFeatureSet of the event.</param>
        /// <param name="drawnStates">The FastDrawnState of the event.</param>
        public AssignFastDrawnStatesEvent(FeatureLayer layer, IFeatureScheme scheme, IFeatureSet dataSet, FastDrawnState[] drawnStates)
        {
            Layer = layer;
            Scheme = scheme;
            DataSet = dataSet;
            DrawnStates = drawnStates;
            Categories = Scheme.GetCategories().ToList();
            Handled = false;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the layer
        /// </summary>
        public FeatureLayer Layer { get; set; }

        /// <summary>
        /// Gets or sets the scheme
        /// </summary>
        public IFeatureScheme Scheme { get; set; }

        /// <summary>
        /// Gets or sets the dataSet
        /// </summary>
        public IFeatureSet DataSet { get; set; }

        /// <summary>
        /// Gets or sets an array of fast-drawn states
        /// </summary>
        public FastDrawnState[] DrawnStates { get; set; }

        /// <summary>
        /// Gets or sets a list of feature categories
        /// </summary>
        public List<IFeatureCategory> Categories { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether AssignFastDrawnStatesEvent
        /// </summary>
        public bool Handled { get; set; }
        #endregion
    }
}