using System;
using System.Collections.Generic;

[Serializable]
public class Code
{
    public int id;
    public string code;
    public int stock;
    public int max_redemptions;
    public List<Transaction> redemptions;
}

