// This file is part of Lisbeth.Bot project
//
// Copyright (C) 2017 Jarl Gullberg
// Copyright (C) 2021 Krzysztof Kupisz - MikyM
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using JetBrains.Annotations;
using MikyM.Common.Utilities.Results.Errors.Bases;

namespace MikyM.Common.Utilities.Results.Errors;

/// <summary>
/// Represents an error arising from an argument being outside of an expected range.
/// </summary>
/// <param name="Name">The name of the argument.</param>
/// <param name="message">The error message.</param>
/// <remarks>Used in place of <see cref="ArgumentOutOfRangeException"/>.</remarks>
public record ArgumentOutOfRangeError
(
    [InvokerParameterName] string Name,
    string message = "Value was outside of the expected range"
) : ArgumentError(Name, message);