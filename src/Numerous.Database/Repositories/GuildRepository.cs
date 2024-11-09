﻿// Copyright (C) Pasi4K5 <https://www.github.com/Pasi4K5>
// This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program. If not, see <https://www.gnu.org/licenses/>.

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Numerous.Database.Context;
using Numerous.Database.Dtos;
using Numerous.Database.Entities;

namespace Numerous.Database.Repositories;

public interface IGuildRepository : IIdRepository<GuildDto, ulong>
{
    Task SetUnverifiedRoleAsync(ulong guildId, ulong? roleId, CancellationToken ct = default);
    Task SetJoinMessageAsync(JoinMessageDto dto, CancellationToken ct = default);
    Task SetMapfeedChannel(ulong guildId, ulong? channelId, CancellationToken ct = default);
}

public sealed class GuildRepository(NumerousDbContext context, IMapper mapper)
    : IdRepository<DbGuild, GuildDto, ulong>(context, mapper), IGuildRepository
{
    public async Task SetUnverifiedRoleAsync(ulong guildId, ulong? roleId, CancellationToken ct = default)
    {
        var guild = await Set.SingleAsync(x => x.Id == guildId, ct);
        guild.UnverifiedRoleId = roleId;
    }

    public async Task SetJoinMessageAsync(JoinMessageDto dto, CancellationToken ct = default)
    {
        var existing = await Context.JoinMessages.FindAsync([dto.GuildId], ct);

        if (existing is null)
        {
            await EnsureChannelExistsAsync<DbMessageChannel>(dto.GuildId, dto.ChannelId, ct);

            var entity = Mapper.Map<DbJoinMessage>(dto);
            await Context.JoinMessages.AddAsync(entity, ct);
        }
        else
        {
            existing.Title = dto.Title;
            existing.Description = dto.Description;
        }
    }

    public async Task SetMapfeedChannel(ulong guildId, ulong? channelId, CancellationToken ct = default)
    {
        var guild = await Set.SingleAsync(x => x.Id == guildId, ct);

        if (channelId is not null)
        {
            await EnsureChannelExistsAsync<DbMessageChannel>(guildId, channelId.Value, ct);
            guild.MapfeedChannelId = channelId;
        }
        else
        {
            guild.MapfeedChannel = null;
        }
    }
}
