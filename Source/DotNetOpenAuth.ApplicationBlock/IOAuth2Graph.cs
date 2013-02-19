﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetOpenAuth.ApplicationBlock
{
    public interface IOAuth2Graph
    {
        string Id { get; }

        Uri Link { get; }

        string Name { get; }

        string FirstName { get; }

        string LastName { get; }

        string Gender { get; }

        string Locale { get; }

        DateTime? BirthdayDT { get; }

        string Email { get; }

        Uri AvatarUrl { get; }

        string UpdatedTime { get; }

        HumanGender GenderEnum { get; }
    }

    public enum HumanGender { Unknown, Male, Female, Other }
}
