﻿using Business.Dtos.Core;
using System.Text.Json.Serialization;

namespace Business.Dtos.v1;
public class ControleAcessoDto : ControleAcessoDtoBase
{
    [JsonIgnore]
    public override Guid Id { get; set; }
}