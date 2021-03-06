using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StereoObfuscator
{
    public class Obfuscator
    {
        public List<Variable> variables = new List<Variable> { };
        public int varIdx = 0;
        public Random random = new Random();
        public string GetRandomAlphaNumeric(int ammount)
        {
            var chars = "Il1Ill1IlllIll11Illl1IllllIlll1IllllIll111Illl111IlllIl1Ill1IlllIll11Illl1IllllIlll1IllllIll111Illl111IlllIl1Ill1IlllIll11Illl1IllllIlll1IllllIll111Illl111IlllIl1Ill1IlllIll11Illl1IllllIlll1IllllIll111Illl111Illl";
            return new string(chars.Select(c => chars[random.Next(chars.Length)]).Take(ammount).ToArray());
        }

        public string getVarName()
        {
            bool valid = true;
            string name = "";
            while (name == "")
            {
                string newName = GetRandomAlphaNumeric(15);
                foreach (Variable var in variables)
                {
                    if (var.newName == newName)
                    {
                        valid = false;
                    }
                }
                if (valid == true)
                {
                    if (newName.IndexOf('1') != -1)
                    {
                        if (newName.IndexOf('1') != 0)
                        {
                            name = newName;
                        }
                        else
                        {
                            return getVarName();
                        }
                    }
                }
                else
                {
                    return getVarName();
                }
            }
            return name;
        }

        public string Obfuscate(string input)
        {
            string returns = input;
            returns = ObfuscateVariables(returns);
            return returns;
        }

        public string ObfuscateVariables(string input)
        {
            string returns = input;
            using (StringReader sr = new StringReader(input))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    bool isVar = false;
                    bool isFunc = false;
                    if (line.Contains("local"))
                    {
                        isVar = true;
                        string varName = Substring.GetStringBetween(line, "local", "=");
                        varName = varName.Replace(" ", "");
                        Variable var = new Variable();
                        var.oldName = varName;
                        var.newName = getVarName();
                        variables.Insert(varIdx, var);
                        varIdx = varIdx + 1;
                        Regex rgx = new Regex(var.oldName);
                        returns = rgx.Replace(returns, var.newName);
                    }

                    if (line.Contains("\"")) //AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
                    {
                        string dumpedstr = null;
                        MatchCollection allMatchResults = null;
                        var noob = "\"";
                        var regexObj = new Regex(noob + @"\w*" + noob);
                        allMatchResults = regexObj.Matches(line);
                        foreach(Match m in allMatchResults)
                        {
                           
                            foreach(char ch in m.Value.Replace("\"", ""))
                            {
                                int x = ch;
                                dumpedstr = dumpedstr + "\\" + x.ToString();
                            }
                            string notsocute = m.Value;
                            Regex rgx = new Regex(notsocute);
                            returns = rgx.Replace(returns, "\"" + dumpedstr + "\"");
                            dumpedstr = null;
                        }
                    }

                    if (line.Contains("function"))
                    {
                        if (isVar == false)
                        {
                            isFunc = true;
                            if (line.Contains("="))
                            {
                                string funcName = line;
                                funcName = funcName.Replace("function", "");
                                int index = funcName.LastIndexOf(" =");
                                if (index > 0)
                                    funcName = funcName.Substring(0, index);
                                Variable func = new Variable();
                                func.oldName = funcName;
                                func.newName = getVarName();
                                variables.Insert(varIdx, func);
                                varIdx = varIdx + 1;
                                Regex rgx = new Regex(func.oldName);
                                returns = rgx.Replace(returns, func.newName);
                            }
                            else
                            {
                                string funcName = Substring.GetStringBetween(line, "function", "(");
                                funcName = funcName.Replace(" ", "");
                                Variable func = new Variable();
                                func.oldName = funcName;
                                func.newName = getVarName();
                                variables.Insert(varIdx, func);
                                varIdx = varIdx + 1;
                                Regex rgx = new Regex(func.oldName);
                                returns = rgx.Replace(returns, func.newName);
                            }
                        }
                    }
                }
            }
            return returns;
        }
    }
}
