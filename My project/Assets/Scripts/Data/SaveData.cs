using System;
using System.Collections.Generic;

[Serializable]
public  class SaveData
{
    public int money;
    public int level = 1;
    public int exp;
    public List<TextDataBox> words = new List<TextDataBox>();
    public List<WandData> wands = new List<WandData>();
}
