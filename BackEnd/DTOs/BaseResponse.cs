﻿namespace BackEnd.DTOs
{
    public class BaseResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSucceeded { get; set; }
    }
}
