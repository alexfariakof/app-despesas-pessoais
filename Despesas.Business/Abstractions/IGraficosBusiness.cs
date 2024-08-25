﻿using Domain.Entities;

namespace Business.Abstractions;
public interface IGraficosBusiness
{
    Grafico GetDadosGraficoByAnoByIdUsuario(Guid idUsuario, DateTime data);
}
