﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ZaloTools.Database;

namespace ZaloTools.Services;

public class DatabaseService : IDatabaseService
{
    private DatabaseLocalContext _localContext;

    public DatabaseService(DatabaseLocalContext localContext)
    {
        _localContext = localContext;
        _localContext.Database.EnsureCreated();
        _localContext.AccountZalos.Load();
    }

    public bool AddZalo(AccountZalo accountZalo)
    {
        _localContext.AccountZalos.Add(accountZalo);
        return _localContext.SaveChanges() > 0;
    }

    public bool UpdateZalo(AccountZalo accountZalo)
    {
        _localContext.AccountZalos.Update(accountZalo);
        return _localContext.SaveChanges() > 0;
    }

    public bool DeleteZalo(AccountZalo accountZalo)
    {
        _localContext.AccountZalos.Remove(accountZalo);
        return _localContext.SaveChanges() > 0;
    }

    public AccountZalo GetZalo(AccountZalo accountZalo)
    {
        return _localContext.AccountZalos.Find(accountZalo);
    }

    public List<AccountZalo> GetAllZalo()
    {
        return _localContext.AccountZalos.ToList();
    }
}