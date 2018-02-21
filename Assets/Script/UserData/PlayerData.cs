﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public static class PlayerData
{
    public static Material PlayerMaterial;

    static XDocument playerData;

    static PlayerData()
    {
        PlayerMaterial = Resources.Load<Material>("Player");
        
        try
        {
            playerData = XDocument.Load("PlayerData.xml");
        }
        catch(FileNotFoundException)
        {
            XDocument xDocument = new XDocument(
                new XElement("PlayerData",
                    new XElement("Color",
                        new XElement("MainColor",
                            new XElement("R", "1"),
                            new XElement("G", "1"),
                            new XElement("B", "1")),
                        new XElement("Metallic", "0"),
                        new XElement("Smoothness", "0.5"),
                        new XElement("Emission",
                            new XElement("R", "0"),
                            new XElement("G", "0"),
                            new XElement("B", "0")))));   

            playerData = xDocument;
            xDocument.Save("PlayerData.xml");

        }
        catch(Exception ex)
        {
            File.WriteAllText("Error_Log", ex.Message);
            Application.Quit();
        }

        XElement x = playerData.Element("PlayerData").Element("Color");

        float m_r = Convert.ToSingle(x.Element("MainColor").Element("R").Value);
        float m_g = Convert.ToSingle(x.Element("MainColor").Element("G").Value);
        float m_b = Convert.ToSingle(x.Element("MainColor").Element("B").Value);

        float metallic = Convert.ToSingle(x.Element("Metallic").Value);
        float smoothness = Convert.ToSingle(x.Element("Smoothness").Value);

        float e_r = Convert.ToSingle(x.Element("Emission").Element("R").Value);
        float e_g = Convert.ToSingle(x.Element("Emission").Element("G").Value);
        float e_b = Convert.ToSingle(x.Element("Emission").Element("B").Value);

        PlayerMaterial.color = new Color(m_r, m_g, m_b);
        PlayerMaterial.SetFloat("_Metallic", metallic);
        PlayerMaterial.SetFloat("_Glossiness", smoothness);
        PlayerMaterial.SetColor("_EmissionColor", new Color(e_r, e_g, e_b));
    }
}
