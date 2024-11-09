﻿// Copyright (C) Pasi4K5 <https://www.github.com/Pasi4K5>
// This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>.

namespace Numerous.Bot.Util;

public static class DateTimeExtensions
{
    public static DateTimeOffset ToOffset(this DateTime dt, TimeZoneInfo? tz = null)
    {
        tz ??= TimeZoneInfo.Utc;

        return new DateTimeOffset(dt, tz.GetUtcOffset(dt)).ToUniversalTime();
    }

    public static DateTime ToDateTime(this DateTimeOffset dto, TimeZoneInfo? tz = null)
    {
        tz ??= TimeZoneInfo.Utc;

        return TimeZoneInfo.ConvertTime(dto, tz).DateTime;
    }
}