using OE.ALGA.Optimalizalas;
using OE.ALGA.Adatszerkezetek;

namespace OE.ALGA.Sandbox
{
//----BRUTE FORCE MOTHERFUCKER--------###############
    class Hely
    {
        public int X { get; set; }
        public int Y { get; set; }
        
    }


    class WifiProblema
    {
        int Xmeret = 20;
        int Ymeret = 20;
        Lista<Hely> routerek;

        public WifiProblema()
        {
            routerek = new LancoltLista<Hely>();
            routerek.Hozzafuz(new Hely() { X = 5, Y = 5 });
            routerek.Hozzafuz(new Hely() { X = 10, Y = 5 });
            routerek.Hozzafuz(new Hely() { X = 10, Y = 10 });
            routerek.Hozzafuz(new Hely() { X = 5, Y = 12 });
            
            //szemleltetes
            for (int i = 1; i <= (Xmeret+1)*(Ymeret+1) ;i++)
            {
                Hely mo = HelyGeneralo(i);
                Console.WriteLine(i+":"+mo.X + "," + mo.Y);
            }
        }

        public Hely HelyGeneralo(int i)
        {
            return new Hely() { X= (i-1)%(Ymeret+1), Y= (i-1)/(Ymeret+1) };
        }

        public float JelErosseg(Hely h)
        {
            float sum = 0;
            routerek.Bejar(
                r => sum += (float)Math.Exp(-Math.Sqrt(Math.Pow(r.X - h.X, 2) + Math.Pow(r.Y - h.Y, 2))/10)
            );
            return sum;

        }

        public Hely IdealisHely()
        {
            NyersEro<Hely> opt3 = new NyersEro<Hely>(
                (Xmeret - 1) * (Ymeret - 1),
                HelyGeneralo,
                JelErosseg
            );
            return opt3.OptimalisMegoldas();
        }
        
        
    }
    internal class Program
    {
        static void Main()
        {
            WifiProblema p= new WifiProblema();
            
            Hely o = p.IdealisHely();
            Console.WriteLine(o.X + "," + o.Y);
            
            
            
           int []A = new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15 ,1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

           NyersEro<int> opt = new NyersEro<int>(
               A.Length,
               x => A[x - 1],
               x => x
           );
           
           Console.WriteLine(opt.OptimalisMegoldas());

//###############################################################
           string szoveg = " Ez egy valamejjest hoszzukás szöveg lehetne";
           string szo = "valami";

           NyersEro<string> opt2 = new NyersEro<string>(
               szoveg.Length - szo.Length + 1,
               i=>szoveg.Substring(i-1,szo.Length),
               x =>
               {
                   int azonos = 0;
                   for (int i = 0; i < szo.Length; i++)
                   {
                       if (x[i]== szo[i])
                           azonos++;
                   }
                   return azonos;
               }
           );
           Console.WriteLine(opt2.OptimalisMegoldas());
//##################################################
           
           
           
           
           
           
           
        }
        
        
        
    }
}
