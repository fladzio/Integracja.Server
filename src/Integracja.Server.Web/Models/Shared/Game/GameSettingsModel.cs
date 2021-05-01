﻿using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Web.Models.Shared.Gamemode;
using Integracja.Server.Web.Models.Shared.Time;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Web.Models.Shared.Game
{
    public class GameSettingsModel
    {
        public string GameName { get; set; }
        public GamemodeModel Gamemode { get; set; } = new GamemodeModel();
        public bool RandomizeQuestionOrder { get; set; }
        public int MaxPlayersCount { get; set; }
        public ICollection<CreateGameUserDto> InvitedUsers { get; set; }

        [DataType(DataType.Time)]
        [Required]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartDate) + "," + nameof(EndDate) + "," + nameof(EndTime))]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartTime) + "," + nameof(EndDate) + "," + nameof(EndTime))]
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset StartDateTime
        {
            get => GetStartDateTimeOffset(StartDate.Offset);
            set
            {
                StartDate = value;
                StartTime = value.TimeOfDay;
            }
        }

        [DataType(DataType.Time)]
        [Required]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartTime) + "," + nameof(EndDate) + "," + nameof(StartDate))]
        public TimeSpan EndTime { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Remote(action: nameof(IGameSettingsValidation.VerifyDates), controller: "Game",
            AdditionalFields = nameof(StartTime) + "," + nameof(StartDate) + "," + nameof(EndTime))]
        public DateTimeOffset EndDate { get; set; }

        public DateTimeOffset EndDateTime
        {
            get => GetEndDateTimeOffset(EndDate.Offset);
            set
            {
                EndDate = value;
                EndTime = value.TimeOfDay;
            }
        }

        public TimeSpan Duration => (EndDateTime - StartDateTime);

        private DateTimeOffset GetEndDateTimeOffset(TimeSpan timeZoneOffset) => new DateTimeOffset(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hours, EndTime.Minutes, EndTime.Seconds, timeZoneOffset);
        private DateTimeOffset GetStartDateTimeOffset(TimeSpan timeZoneOffset) => new DateTimeOffset(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hours, StartTime.Minutes, StartTime.Seconds, timeZoneOffset);
    }
}
