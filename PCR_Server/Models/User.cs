﻿namespace PCR_Server.Modesl
{
    public class User
    {
        public string Username { get; set; }
        public string ConnectionId { get; set; }

        public User(string username, string connectionId)
        {
            Username = username;
            ConnectionId = connectionId;
        }
    }
}