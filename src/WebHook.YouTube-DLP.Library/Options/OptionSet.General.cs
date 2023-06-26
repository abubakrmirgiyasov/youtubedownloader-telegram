﻿// <auto-generated>
// This code was partially generated by a tool.
// </auto-generated>

using System;

namespace YoutubeDLSharp.Options
{
    public partial class OptionSet
    {
        private Option<bool> help = new Option<bool>("-h", "--help");
        private Option<bool> version = new Option<bool>("--version");
        private Option<bool> update = new Option<bool>("-U", "--update");
        private Option<bool> noUpdate = new Option<bool>("--no-update");
        private Option<bool> ignoreErrors = new Option<bool>("-i", "--ignore-errors");
        private Option<bool> noAbortOnError = new Option<bool>("--no-abort-on-error");
        private Option<bool> abortOnError = new Option<bool>("--abort-on-error");
        private Option<bool> dumpUserAgent = new Option<bool>("--dump-user-agent");
        private Option<bool> listExtractors = new Option<bool>("--list-extractors");
        private Option<bool> extractorDescriptions = new Option<bool>("--extractor-descriptions");
        private Option<string> useExtractors = new Option<string>("--use-extractors");
        private Option<string> defaultSearch = new Option<string>("--default-search");
        private Option<bool> ignoreConfig = new Option<bool>("--ignore-config");
        private Option<bool> noConfigLocations = new Option<bool>("--no-config-locations");
        private MultiOption<string> configLocations = new MultiOption<string>("--config-locations");
        private Option<bool> flatPlaylist = new Option<bool>("--flat-playlist");
        private Option<bool> noFlatPlaylist = new Option<bool>("--no-flat-playlist");
        private Option<bool> liveFromStart = new Option<bool>("--live-from-start");
        private Option<bool> noLiveFromStart = new Option<bool>("--no-live-from-start");
        private Option<string> waitForVideo = new Option<string>("--wait-for-video");
        private Option<bool> noWaitForVideo = new Option<bool>("--no-wait-for-video");
        private Option<bool> markWatched = new Option<bool>("--mark-watched");
        private Option<bool> noMarkWatched = new Option<bool>("--no-mark-watched");
        private Option<bool> noColors = new Option<bool>("--no-colors");
        private Option<string> compatOptions = new Option<string>("--compat-options");
        private Option<string> alias = new Option<string>("--alias");

        /// <summary>
        /// Print this help text and exit
        /// </summary>
        public bool Help { get => help.Value; set => help.Value = value; }
        /// <summary>
        /// Print program version and exit
        /// </summary>
        public bool Version { get => version.Value; set => version.Value = value; }
        /// <summary>
        /// Update this program to the latest version
        /// </summary>
        public bool Update { get => update.Value; set => update.Value = value; }
        /// <summary>
        /// Do not check for updates (default)
        /// </summary>
        public bool NoUpdate { get => noUpdate.Value; set => noUpdate.Value = value; }
        /// <summary>
        /// Ignore download and postprocessing errors.
        /// The download will be considered successful
        /// even if the postprocessing fails
        /// </summary>
        public bool IgnoreErrors { get => ignoreErrors.Value; set => ignoreErrors.Value = value; }
        /// <summary>
        /// Continue with next video on download errors;
        /// e.g. to skip unavailable videos in a
        /// playlist (default)
        /// </summary>
        public bool NoAbortOnError { get => noAbortOnError.Value; set => noAbortOnError.Value = value; }
        /// <summary>
        /// Abort downloading of further videos if an
        /// error occurs (Alias: --no-ignore-errors)
        /// </summary>
        public bool AbortOnError { get => abortOnError.Value; set => abortOnError.Value = value; }
        /// <summary>
        /// Display the current user-agent and exit
        /// </summary>
        public bool DumpUserAgent { get => dumpUserAgent.Value; set => dumpUserAgent.Value = value; }
        /// <summary>
        /// List all supported extractors and exit
        /// </summary>
        public bool ListExtractors { get => listExtractors.Value; set => listExtractors.Value = value; }
        /// <summary>
        /// Output descriptions of all supported
        /// extractors and exit
        /// </summary>
        public bool ExtractorDescriptions { get => extractorDescriptions.Value; set => extractorDescriptions.Value = value; }
        /// <summary>
        /// Extractor names to use separated by commas.
        /// You can also use regexes, &quot;all&quot;, &quot;default&quot;
        /// and &quot;end&quot; (end URL matching); e.g. --ies
        /// &quot;holodex.*,end,youtube&quot;. Prefix the name
        /// with a &quot;-&quot; to exclude it, e.g. --ies
        /// default,-generic. Use --list-extractors for
        /// a list of extractor names. (Alias: --ies)
        /// </summary>
        public string UseExtractors { get => useExtractors.Value; set => useExtractors.Value = value; }
        /// <summary>
        /// Use this prefix for unqualified URLs. E.g.
        /// &quot;gvsearch2:python&quot; downloads two videos from
        /// google videos for the search term &quot;python&quot;.
        /// Use the value &quot;auto&quot; to let yt-dlp guess
        /// (&quot;auto_warning&quot; to emit a warning when
        /// guessing). &quot;error&quot; just throws an error. The
        /// default value &quot;fixup_error&quot; repairs broken
        /// URLs, but emits an error if this is not
        /// possible instead of searching
        /// </summary>
        public string DefaultSearch { get => defaultSearch.Value; set => defaultSearch.Value = value; }
        /// <summary>
        /// Don&#x27;t load any more configuration files
        /// except those given by --config-locations.
        /// For backward compatibility, if this option
        /// is found inside the system configuration
        /// file, the user configuration is not loaded.
        /// (Alias: --no-config)
        /// </summary>
        public bool IgnoreConfig { get => ignoreConfig.Value; set => ignoreConfig.Value = value; }
        /// <summary>
        /// Do not load any custom configuration files
        /// (default). When given inside a configuration
        /// file, ignore all previous --config-locations
        /// defined in the current file
        /// </summary>
        public bool NoConfigLocations { get => noConfigLocations.Value; set => noConfigLocations.Value = value; }
        /// <summary>
        /// Location of the main configuration file;
        /// either the path to the config or its
        /// containing directory (&quot;-&quot; for stdin). Can be
        /// used multiple times and inside other
        /// configuration files
        /// </summary>
        public MultiValue<string> ConfigLocations { get => configLocations.Value; set => configLocations.Value = value; }
        /// <summary>
        /// Do not extract the videos of a playlist,
        /// only list them
        /// </summary>
        public bool FlatPlaylist { get => flatPlaylist.Value; set => flatPlaylist.Value = value; }
        /// <summary>
        /// Extract the videos of a playlist
        /// </summary>
        public bool NoFlatPlaylist { get => noFlatPlaylist.Value; set => noFlatPlaylist.Value = value; }
        /// <summary>
        /// Download livestreams from the start.
        /// Currently only supported for YouTube
        /// (Experimental)
        /// </summary>
        public bool LiveFromStart { get => liveFromStart.Value; set => liveFromStart.Value = value; }
        /// <summary>
        /// Download livestreams from the current time
        /// (default)
        /// </summary>
        public bool NoLiveFromStart { get => noLiveFromStart.Value; set => noLiveFromStart.Value = value; }
        /// <summary>
        /// Wait for scheduled streams to become
        /// available. Pass the minimum number of
        /// seconds (or range) to wait between retries
        /// </summary>
        public string WaitForVideo { get => waitForVideo.Value; set => waitForVideo.Value = value; }
        /// <summary>
        /// Do not wait for scheduled streams (default)
        /// </summary>
        public bool NoWaitForVideo { get => noWaitForVideo.Value; set => noWaitForVideo.Value = value; }
        /// <summary>
        /// Mark videos watched (even with --simulate)
        /// </summary>
        public bool MarkWatched { get => markWatched.Value; set => markWatched.Value = value; }
        /// <summary>
        /// Do not mark videos watched (default)
        /// </summary>
        public bool NoMarkWatched { get => noMarkWatched.Value; set => noMarkWatched.Value = value; }
        /// <summary>
        /// Do not emit color codes in output (Alias:
        /// --no-colours)
        /// </summary>
        public bool NoColors { get => noColors.Value; set => noColors.Value = value; }
        /// <summary>
        /// Options that can help keep compatibility
        /// with youtube-dl or youtube-dlc
        /// configurations by reverting some of the
        /// changes made in yt-dlp. See &quot;Differences in
        /// default behavior&quot; for details
        /// </summary>
        public string CompatOptions { get => compatOptions.Value; set => compatOptions.Value = value; }
        /// <summary>
        /// Create aliases for an option string. Unless
        /// an alias starts with a dash &quot;-&quot;, it is
        /// prefixed with &quot;--&quot;. Arguments are parsed
        /// according to the Python string formatting
        /// mini-language. E.g. --alias get-audio,-X
        /// &quot;-S=aext:{0},abr -x --audio-format {0}&quot;
        /// creates options &quot;--get-audio&quot; and &quot;-X&quot; that
        /// takes an argument (ARG0) and expands to
        /// &quot;-S=aext:ARG0,abr -x --audio-format ARG0&quot;.
        /// All defined aliases are listed in the --help
        /// output. Alias options can trigger more
        /// aliases; so be careful to avoid defining
        /// recursive options. As a safety measure, each
        /// alias may be triggered a maximum of 100
        /// times. This option can be used multiple
        /// times
        /// </summary>
        public string Alias { get => alias.Value; set => alias.Value = value; }
    }
}