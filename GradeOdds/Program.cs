using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CenterSpace;
using CenterSpace.Free;

namespace GradeOdds
{
    class Program
    {

        const float PASSINGGRADE = 5.5F;
        static float kans = 0;
        static float[] HavoDU = { 0.615F, 0.22661F };
        static float[] HavoWA = { 1.31667F, 0.03424F };
        static float[] HavoEN = { 0.66F, 0.21937F };
        static float[] HavoGS = { 1.17333F, 0.08638F };
        static float[] HavoEC = { 1.42143F, 0.05412F };
        static float[] HavoAK = { 1.16F, 0.06042F };
        static float[] HavoBI = { 1, 0.08727F };
        static float[] HavoWB = { 1.56667F, 0.08061F};
        static float[] HavoKA = { 1.14286F, 0.11157F };
        static float[] HavoFR = { 0.475F, 0.20303F};
        static float[] HavoNA = {1.21667F, 0.12697F};
        static float[] HavoNL = {1.23F, 0.06853F};
        static float[] HavoSK = { 1.63333F, 0.1097F };
        static int invoer = 0;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Ten eerste wil ik u bedanken voor het testen van dit programma.");
            Console.WriteLine("Het heeft erg veel moeite gekost om alles aan de praat te krijgen,");
            Console.WriteLine("maar gelukkig resulteert het wel in een functionerend programma.");
            Console.WriteLine("");
            Console.WriteLine("Dit programma is maar een proof of concept, gemikt maar naar een handvol");
            Console.WriteLine("individuen om te testen of alles werkt. Later zal het system overgezet worden");
            Console.WriteLine("Naar een openbaar beschikbare webapplicatie.");
            Console.WriteLine("");
            Console.WriteLine("Geschreven door Tobias Draisma op 13-5-2019");

            for (int x = 0; x < 2; x++)
            {
                Console.WriteLine("");
            }

            Console.WriteLine("Druk op enter om door te gaan.");
            Console.ReadLine();
            Console.Clear();

            //<Examen_Selectie>
            bool s1 = false;
            while (!s1)
            {
                printOpties();
                if (int.TryParse(Console.ReadLine(), out invoer) & (invoer < 39) & (invoer > 0))
                {
                    s1 = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("De invoer is ongeldig. Probeer het opnieuw.");
                }
            }
            Console.Clear();
            //</Examen_Selectie>

            bool valid = false;
            float beschikbaar = 0;
            float gehaald = 0;
            while (!valid)
            {
                
                beschikbaar = KrijgBeschikbaar();
                gehaald = krijgGehaald();
                if (gehaald > beschikbaar)
                {
                    Console.Clear();
                    Console.WriteLine("Het gehaalde hoeveelheid punten is hoger dan de mogelijk haalbare hoeveelheid punten;");
                    Console.WriteLine("geef de invoer AUB opnieuw.");
                }
                else {
                    valid = true;
                }
            }

            float precijfer = (gehaald / beschikbaar) * 9;
            precijfer = (float)Math.Round(precijfer, 1);
            

            float minimum;
            minNTerm(precijfer, out minimum);
            minimum = (float)Math.Round(minimum, 1);
            Console.WriteLine("Uw gehaalde cijfer (zonder N term) is " + precijfer + ".");
            Console.WriteLine("Dit wilt zeggen dat u een minimum N term nodig heeft van " + minimum + ".");

            switch (invoer)
            {
                case 13:
                    kans = KrijgKans(HavoAK, minimum);
                    break;
                case 14:
                    kans = KrijgKans(HavoBI, minimum);
                    break;
                case 15:
                    kans = KrijgKans(HavoDU, minimum);
                    break;
                case 16:
                    kans = KrijgKans(HavoEC, minimum);
                    break;
                case 17:
                    kans = KrijgKans(HavoEN, minimum);
                    break;
                case 18:
                    kans = KrijgKans(HavoFR, minimum);
                    break;
                case 19:
                    kans = KrijgKans(HavoGS, minimum);
                    break;
                case 20:
                    kans = KrijgKans(HavoKA, minimum);
                    break;
                case 21:
                    kans = KrijgKans(HavoNA, minimum);
                    break;
                case 22:
                    kans = KrijgKans(HavoNL, minimum);
                    break;
                case 23:
                    kans = KrijgKans(HavoSK, minimum);
                    break;
                case 24:
                    kans = KrijgKans(HavoWA, minimum);
                    break;
                case 25:
                    kans = KrijgKans(HavoWB, minimum);
                    break;


                default:
                    Console.Clear();
                    Console.WriteLine("Invoer is gesteld op een invalide waarde. Het programma gaat nu afsluiten.");
                    Console.ReadLine();
                    Environment.Exit(1);
                    break;
                
            }

            
            Console.WriteLine("De kans dat de N term deze waarde of hoger zal hebben is " + kans +"%");
            Console.ReadLine();
            

        }

        static float KrijgBeschikbaar()
        {

            float beschikbaar = 0;
            bool success = false;
            while (!success)
            {
                Console.WriteLine("Hoeveel punten kon u totaal voor de toets halen?");
                if (float.TryParse(Console.ReadLine(), out beschikbaar))
                {
                    success = true;
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Uw invoer is niet herkend als cijfer. Voer het AUB nog een keer in.");
                    Console.WriteLine("");
                }
            }
            Console.Clear();
            return beschikbaar;
        }

        static float krijgGehaald()
        {
            float gehaald = 0;
            bool success = false;
            while (!success)
            {
                Console.WriteLine("Hoeveel punten heeft u voor de toets gehaald?");
                if (float.TryParse(Console.ReadLine(), out gehaald))
                {
                    success = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Uw invoer is niet herkend als cijfer. Voer het AUB nog een keer in.");
                    Console.WriteLine("");
                }
            }
            Console.Clear();
            return gehaald;
        }

        static void minNTerm(float tussencijfer, out float min)
        {

            //float t = 0;

            //while (tussencijfer + t < PASSINGGRADE)       //Oude methode om de minimum N term te berekenen.
            //{

            //    t = t + 0.1F;

            //}

            //min = t;

            float r = PASSINGGRADE - tussencijfer;
            if (r > 0)
            {
                min = r;
            }
            else
            {
                min = 0;
            }
        }
        static float KrijgKans(float[] vak, float minimum)
        {
            NormalDist dist = new NormalDist(vak[0], vak[1]);
            float kans = 1 - (float)dist.CDF((double)minimum);
            return (float)Math.Round(kans * 100,1);
        }
        static void printOpties()
        {
            Console.WriteLine("Selecteer AUB een examen.");
            Console.WriteLine("");
            Console.WriteLine("VMBO:                Havo:                   VWO:");
            Console.WriteLine("1. Aardrijkskunde  X 13. Aardrijkskunde      25. Aardrijkskunde X");
            Console.WriteLine("2. Biologie        X 14. Biologie            26. Biologie X");
            Console.WriteLine("3. Duits           X 15. Duits               27. Duits X");
            Console.WriteLine("4. Economie        X 16. Economie            28. Economie X");
            Console.WriteLine("5. Engels          X 17. Engels              29. Engels X");
            Console.WriteLine("6. Frans           X 18. Frans               30. Frans X");
            Console.WriteLine("7. Geschiedenis    X 19. Geschiedenis        31. Geschiedenis X");
            Console.WriteLine("8. Wiskunde        X 20. Kunst (Algemeen)    32. Kunst (Algemeen) X");
            Console.WriteLine("9. NaSk 1          X 21. Natuurkunde         33. Natuurkunde X");
            Console.WriteLine("10. Nederlands     X 22. Nederlands          33. Nederlands X");
            Console.WriteLine("11. NaSk 2         X 23. Scheikunde          34. Scheikunde X");
            Console.WriteLine("12. Wiskunde       X 24. Wikunde A           35. Wikunde A X");
            Console.WriteLine("                   X 25. Wiskunde B          37. Wiksunde B X");
            Console.WriteLine("                                             38. Wiskunde D X");
            Console.WriteLine("");
            Console.WriteLine("(Een kruis achter de naam van het examen indiceert dat deze nog niet verwerkt is in dit programma.)");
            Console.WriteLine("");
            Console.WriteLine("Voer het corresponderende getal in.");
        }
    }
}
