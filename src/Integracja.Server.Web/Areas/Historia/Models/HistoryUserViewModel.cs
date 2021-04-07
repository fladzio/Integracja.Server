﻿using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Historia.Models
{
    public class HistoryUserViewModel
    {
        public string Username { get; set; }
        public int Points { get; set; }
        public List<HistoryUserInfo> HistoryGameUserInfo { get; set; }
    }

    public class HistoryUserInfo
    {
        public string questionContent;
        public List<string> answers;
        public int correctAnswerId;
        public int userAnswerId;
        public int pointsReceived;
        public int positivePoints;
        public int negativePoints;
    }
}
