﻿using Application.Interfaces;

namespace Application.Models;

public class CandidateDto : IDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Comment { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTimeOffset? AvailableFrom { get; set; }

    public DateTimeOffset? AvailableTo { get; set; }

    public string? LinkedinUrl { get; set; }

    public string? GithubUrl { get; set; }
}