﻿// <auto-generated>
// This code was partially generated by a tool.
// </auto-generated>

using System;

namespace YoutubeDLSharp.Options
{
    public partial class OptionSet
    {
        private Option<bool> writeThumbnail = new Option<bool>("--write-thumbnail");
        private Option<bool> noWriteThumbnail = new Option<bool>("--no-write-thumbnail");
        private Option<bool> writeAllThumbnails = new Option<bool>("--write-all-thumbnails");
        private Option<bool> listThumbnails = new Option<bool>("--list-thumbnails");

        /// <summary>
        /// Write thumbnail image to disk
        /// </summary>
        public bool WriteThumbnail { get => writeThumbnail.Value; set => writeThumbnail.Value = value; }
        /// <summary>
        /// Do not write thumbnail image to disk
        /// (default)
        /// </summary>
        public bool NoWriteThumbnail { get => noWriteThumbnail.Value; set => noWriteThumbnail.Value = value; }
        /// <summary>
        /// Write all thumbnail image formats to disk
        /// </summary>
        public bool WriteAllThumbnails { get => writeAllThumbnails.Value; set => writeAllThumbnails.Value = value; }
        /// <summary>
        /// List available thumbnails of each video.
        /// Simulate unless --no-simulate is used
        /// </summary>
        public bool ListThumbnails { get => listThumbnails.Value; set => listThumbnails.Value = value; }
    }
}
