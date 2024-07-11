﻿using Business.Abstractions;
using Domain.Entities;
using Repository.Persistency.Abstractions;

namespace Business.Implementations;
public class GraficosBusinessImpl : IGraficosBusiness
{
    private readonly IGraficosRepositorio _repositorio;

    public GraficosBusinessImpl(IGraficosRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public Grafico GetDadosGraficoByAnoByIdUsuario(Guid idUsuario, DateTime data)
    {
        return _repositorio.GetDadosGraficoByAno(idUsuario, data);
    }
}
