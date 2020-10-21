using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

/// @author Jouni Friman
/// @version Konsoliversio beta 20.09.2020
/// <summary>
/// Proof of Concept - Konsoliversiona tehty logiikan testaus hedelmäpelistä joka toteutetaan graafisena myöhemmin.
/// </summary>
public class Hedelmapeli_konsole
{
    /// <summary>
    /// 
    /// </summary>
    public static void Main()
    {
        bool exit = false;
        int[] arvotutNumerot = new int[9];                  //TODO: Muuta vakiot fiksuiksi, oliot olis varmaan siistejä.
        int rahat = 20;
        int panos = 1;

        while (exit == false)                                //Mainloop, toistaa itseään kunnes suljetaan TODO:lopetus pelille ilman vakiota
        {
            int komento = PelaajaKomento(panos);                      //Ottaa pelaajan komennon vastaan ja iffeillä päätetään mitä tapahtuu. TODO: Pohdi olisko joku fiksumpi systeemi (?)

            if (komento == 1) rahat = Kaynnista(arvotutNumerot, rahat, panos);              //Komennon perusteella käynnistää pelin
            if (komento == 2) panos = Panosta(rahat);                         //Komennon perusteella nostaa tai laskee panosta.
            if (komento == 3) exit = Lopeta();                  //Lopettaa silmukan, ei välttämättä tarvitsisi aliohjelmaa, mutta "moduläärisempi" näin :-)

            //TODO : jos väärä komento
        }
    }

    public static int PelaajaKomento(int panos)
    {
        int komento = 0;

        Console.WriteLine("Anna numerona komento mitä haluat tehdä :"); //Tulostetaan komennot näytille
        Console.WriteLine("1) Käynnistä Hedelmäpeli");
        Console.WriteLine("2) Muuta panosta, panos nyt > {0}. kolikkoa", panos);
        Console.WriteLine("3) Lopeta peli");
        Console.Write(">");

        return komento = Convert.ToInt32(Console.ReadLine()); //Palauttaa int arvona pelaajan valitseman komennon
    }

    public static int Kaynnista(int [] arvotutNumerot, int rahat, int panos)
    {
        if(rahat <= 0) { Console.WriteLine("Rahasi ovat loppu. Voit lopettaa pelin."); return rahat; }

        Random rnd = new Random();              //Määritetään random jota käytetään arpomaan hedelmäpelin tulokset
        int rahatPanos = rahat - panos;

        for (int i=0; i<arvotutNumerot.Length; i++)
        {
            arvotutNumerot[i] = rnd.Next(1,5);      //Arvotaan tulokset taulukkoon
        }

        Grafiikka(arvotutNumerot, rahatPanos);                //Arvonnan jälkeen kutsutaan grafiikka aliohjelmaa joka tulostaa pelin näytölle
        int palautus = VoitonTarkistus(arvotutNumerot, rahatPanos, panos); //Arvonnan jälkeen voitontarkistus

        return palautus; //Lopulta palauttaa rahatilanteen pääohjelmaan

    }

    public static int Panosta(int rahat) //Panoksen asetus
    {
        int panos = 1;
        Console.WriteLine("Aseta panos:");
        panos = Convert.ToInt32(Console.ReadLine());
        if (rahat >= panos) return panos;                   //Rahojen riittävyyden tarkistus
        else
        {
            Console.Write("Kolikkosi eivät riitä, panos on nyt 1 kolikko");
            return 1;
        }
    }

    public static void Grafiikka(int [] arvotutNumerot, int rahat)
    {
        int laskuri = 0;
        Console.WriteLine("*********************");
        for (int i = 0; i < arvotutNumerot.Length; i++)
        {
            Console.Write("** " + arvotutNumerot[i] + " **");
            laskuri++;
            if (laskuri == 3)
            {
                Console.WriteLine("");                      //Maailman surkein spagettikoodiviritys halp, jypelissä sitten jotenkin fiksummin
                laskuri = 0;                                //Yritetään siis tulostaa koodi nätisti kolmelle eri riville
            }
        }
        Console.WriteLine("*********************");
    }

    public static int VoitonTarkistus(int [] arvotutNumerot, int rahat, int panos) 
    {
        int voitto = 0;

        if (arvotutNumerot[0] == arvotutNumerot[1] && arvotutNumerot[1] == arvotutNumerot[2]) //Tarkastetaan onko kolmen rivi samoja numeroita
        {
            if (arvotutNumerot[0] == 1) voitto += (panos * 5); //Jos voitto tulee ykkösillä, voitto on isompi
            else voitto += (panos * 3);                     //Muilla pienempi
        }
        if (arvotutNumerot[3] == arvotutNumerot[4] && arvotutNumerot[4] == arvotutNumerot[5])
        {
            if (arvotutNumerot[3] == 1) voitto += (panos * 5);
            else voitto += (panos * 3);
        }
        if (arvotutNumerot[6] == arvotutNumerot[7] && arvotutNumerot[7] == arvotutNumerot[8])
        {
            if (arvotutNumerot[6] == 1) voitto += (panos * 5);
            else voitto += (panos * 3);
        }

        if (arvotutNumerot[0] == arvotutNumerot[4] && arvotutNumerot[4] == arvotutNumerot[8]) voitto += (panos * 2);
        if (arvotutNumerot[6] == arvotutNumerot[4] && arvotutNumerot[4] == arvotutNumerot[2]) voitto += (panos * 2);

        rahat += voitto;

        if (voitto != 0) Console.WriteLine("---------Voitit {0} kolikkoa!-----------", voitto);
        Console.WriteLine("Sinulla on nyt {0} kolikkoa", rahat);

        return rahat;
    }

    public static bool Lopeta()
    {
        return true;
    }

}





//TODO: Lisää eri voittoluokkia  -----> näiden jälkeen siirto jypeliin. TESTITIESIT LISÄÄÄÄÄ ÄTESTITÄÄÄTTS