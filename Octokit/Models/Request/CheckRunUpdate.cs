﻿using Octokit.Internal;
using System;

namespace Octokit
{
    public class CheckRunUpdate
    {
        public string Name { get; set; }
        public string DetailsUrl { get; set; }
        public string ExternalId { get; set; }
        public CheckStatus? Status { get; set; }
        public DateTimeOffset StartedAt { get; set; }
        public CheckConclusion Conclusion { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public CheckRunOutput Output { get; set; }
    }
    
    public enum CheckStatus
    {
        [Parameter(Value = "queued")]
        Queued,

        [Parameter(Value = "in_progress")]
        InProgress,

        [Parameter(Value = "completed")]
        Completed,
    }
    
    public enum CheckConclusion
    {
        [Parameter(Value = "success")]
        Success,
        
        [Parameter(Value = "failure")]
        Failure,

        [Parameter(Value = "neutral")]
        Neutral,

        [Parameter(Value = "cancelled")]
        Cancelled,

        [Parameter(Value = "timed_out")]
        TimedOut,

        [Parameter(Value = "action_required")]
        ActionRequired,
    }
}
