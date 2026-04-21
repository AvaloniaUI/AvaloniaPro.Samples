using System;

namespace Avalonia.Media.Demo.Models
{
    public class Statistics
    {
        private readonly MediaStatistics? _mediaStats;
        private readonly DateTime _timestamp;
        private readonly float _frameRate;
        private readonly int _displayedFrames;

        public Statistics(MediaStatistics? mediaStats, Statistics? previous = null)
        {
            _mediaStats = mediaStats;
            _displayedFrames = DisplayedPictures;
            _timestamp = DateTime.UtcNow;

            if (mediaStats?.FramesPerSecond == null && previous != null)
            {
                if ((_timestamp - previous._timestamp).TotalMilliseconds > 1000)
                {
                    var timeDiff = (_timestamp - previous._timestamp).TotalMilliseconds;
                    var frameDiff = _displayedFrames - previous._displayedFrames;
                    var frameRateMicroS = frameDiff / timeDiff;

                    _frameRate = (float)(frameRateMicroS * 1_000);

                }
                else
                {
                    _displayedFrames = previous._displayedFrames;
                    _timestamp = previous._timestamp;
                    _frameRate = previous._frameRate;
                }
            }
            else
                _frameRate = mediaStats?.FramesPerSecond ?? 0;
        }

        public string ReadBytes
        {
            get
            {
                var read = _mediaStats?.ReadBytes ?? 0;
                if (read < 1024)
                    return $"{read} B";
                if (read < 1024 * 1024)
                    return $"{read / (1024)} KB";

                return $"{read / (1024 * 1024)} MB";
            }
        }

        public string InputBitRate => $"{(_mediaStats?.InputBitRate ?? 0 * 1024.0):F2} Kbit/s";

        public int DemuxReadBytes => _mediaStats?.DemuxReadBytes ?? 0;
        public string DemuxBitRate => $"{(_mediaStats?.DemuxBitRate ?? 0 * 1024.0):F2} Kbit/s";
        public int DemuxCorrupted => _mediaStats?.DemuxCorrupted ?? 0;
        public int DemuxDiscontinuity => _mediaStats?.DemuxDiscontinuity ?? 0;

        public int DecodedVideo => _mediaStats?.DecodedVideo ?? 0;
        public int DecodedAudio => _mediaStats?.DecodedAudio ?? 0;

        public int DisplayedPictures => _mediaStats?.DisplayedPictures ?? 0;
        public int LostPictures => _mediaStats?.LostPictures ?? 0;

        public int PlayedAudioBuffers => _mediaStats?.PlayedAudioBuffers ?? 0;
        public int LostAudioBuffers => _mediaStats?.LostAudioBuffers ?? 0;

        public int SentPackets => _mediaStats?.SentPackets ?? 0;
        public int SentBytes => _mediaStats?.SentBytes ?? 0;
        public float SendBitRate => _mediaStats?.SendBitRate ?? 0;

        public string FrameRate => $"{_frameRate:F2}";
    }
}
