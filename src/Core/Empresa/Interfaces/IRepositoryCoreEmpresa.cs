﻿using System.Linq.Expressions;
using IDN.Core.Empresa.Models;

namespace IDN.Core.Empresa.Interfaces;

public interface IRepositoryCoreEmpresa : IRepositoryCore<MEmpresa>
{
    IAsyncEnumerable<MEmpresa> DoStoredProcedure(string param);
    IAsyncEnumerable<MEmpresa> DoListAsync(Expression<Func<MEmpresa, bool>>? param = null);
}

