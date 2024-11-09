// using OE.ALGA.Optimalizalas;
// using OE.ALGA.Adatszerkezetek;
//
// namespace OE.ALGA.Sandbox
// {
//
//     internal class Program
//     {
//         // static void Main(string[] args)
//         // {
//         //
//         //     int n =8 ;
//         //     int[] M = new int [8];
//         //     int[,]R = new int[n, n];
//         //
//         //     for (int i = 0; i < n; i++)
//         //     {
//         //         M[i] = n;
//         //         for (int j = 0; j < n; j++)
//         //         {
//         //             R[i, j] = j;
//         //         }
//         //
//         //     }
//         //
//         //     VisszalepesesKereses<int> opt = new VisszalepesesKereses<int>(
//         //         n,
//         //         M,
//         //         R,
//         //         (szint, r )=>true,
//         //         (szint, r, E) =>
//         //         {
//         //             for (int i = 0; i < szint; i++)
//         //             
//         //                 if (E[i] == r || Math.Abs(szint-i) == Math.Abs(r-E[i])) return false;
//         //                 return true;
//         //             
//         //         }
//         //         );
//         //     int[] mo = opt.EgyMegoldas();
//         //
//         //     for (int s = n-1; s >=0 ; s--)
//         //     {
//         //         for (int o = 0; o < n; o++)
//         //         
//         //             Console.Write(mo[o] == s ? "W " : "[]");
//         //         Console.WriteLine();    
//         //         
//         //     }
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//             //######################Egyszerű backtrack###################################
//             // int[] M = new int[6];
//             // string[,] R = new string[6, 3];
//             // M[0] = 2; R[0,0]= "miklós"; R[0,1]= "klaudia";
//             // M[1] = 2; R[1,0]= "miklós"; R[1,1]= "andras";
//             // M[2] = 2; R[2,0]= "andras"; R[2,1]= "zsolt";
//             // M[3] = 3; R[3,0]= "geza"; R[3,1]= "zsolt"; R[3,2]="palika";
//             // M[4] = 2; R[4,0]= "geza"; R[4,1]= "andras";
//             // M[5] = 2; R[5,0]= "miklós"; R[5,1]= "geza";
//             //
//             //
//             // VisszalepesesKereses<string> opt = new VisszalepesesKereses<string>(
//             //     6,
//             //     M,
//             //     R,
//             //     (szint, r) => true,
//             //     (szint, r, E) =>
//             //     {
//             //         for (int i = 0; i < szint; i++)
//             //         
//             //             if (E[i] == r) return false;
//             //             return true;
//             //         
//             //     }
//             // );
//             //
//             // string[] m  = opt.EgyMegoldas();
//             // foreach (string s in m)
//             // {
//             //     Console.WriteLine(s);
//             // }
//
//
//         }
//     }
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
//     
// //----BRUTE FORCE MOTHERFUCKER--------###############
// //     class Hely
// //     {
// //         public int X { get; set; }
// //         public int Y { get; set; }
// //         
// //     }
// //
// //
// //     class WifiProblema
// //     {
// //         int Xmeret = 20;
// //         int Ymeret = 20;
// //         Lista<Hely> routerek;
// //
// //         public WifiProblema()
// //         {
// //             routerek = new LancoltLista<Hely>();
// //             routerek.Hozzafuz(new Hely() { X = 5, Y = 5 });
// //             routerek.Hozzafuz(new Hely() { X = 10, Y = 5 });
// //             routerek.Hozzafuz(new Hely() { X = 10, Y = 10 });
// //             routerek.Hozzafuz(new Hely() { X = 5, Y = 12 });
// //             
// //             //szemleltetes
// //             for (int i = 1; i <= (Xmeret+1)*(Ymeret+1) ;i++)
// //             {
// //                 Hely mo = HelyGeneralo(i);
// //                 Console.WriteLine(i+":"+mo.X + "," + mo.Y);
// //             }
// //         }
// //
// //         public Hely HelyGeneralo(int i)
// //         {
// //             return new Hely() { X= (i-1)%(Ymeret+1), Y= (i-1)/(Ymeret+1) };
// //         }
// //
// //         public float JelErosseg(Hely h)
// //         {
// //             float sum = 0;
// //             routerek.Bejar(
// //                 r => sum += (float)Math.Exp(-Math.Sqrt(Math.Pow(r.X - h.X, 2) + Math.Pow(r.Y - h.Y, 2))/10)
// //             );
// //             return sum;
// //
// //         }
// //
// //         public Hely IdealisHely()
// //         {
// //             NyersEro<Hely> opt3 = new NyersEro<Hely>(
// //                 (Xmeret - 1) * (Ymeret - 1),
// //                 HelyGeneralo,
// //                 JelErosseg
// //             );
// //             return opt3.OptimalisMegoldas();
// //         }
// //         
// //         
// //     }
// //     internal class Program
// //     {
// //         static void Main()
// //         {
// //             WifiProblema p= new WifiProblema();
// //             
// //             Hely o = p.IdealisHely();
// //             Console.WriteLine(o.X + "," + o.Y);
// //             
// //             
// //             
// //            int []A = new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15 ,1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
// //
// //            NyersEro<int> opt = new NyersEro<int>(
// //                A.Length,
// //                x => A[x - 1],
// //                x => x
// //            );
// //            
// //            Console.WriteLine(opt.OptimalisMegoldas());
// //
// // //###############################################################
// //            string szoveg = " Ez egy valamejjest hoszzukás szöveg lehetne";
// //            string szo = "valami";
// //
// //            NyersEro<string> opt2 = new NyersEro<string>(
// //                szoveg.Length - szo.Length + 1,
// //                i=>szoveg.Substring(i-1,szo.Length),
// //                x =>
// //                {
// //                    int azonos = 0;
// //                    for (int i = 0; i < szo.Length; i++)
// //                    {
// //                        if (x[i]== szo[i])
// //                            azonos++;
// //                    }
// //                    return azonos;
// //                }
// //            );
// //            Console.WriteLine(opt2.OptimalisMegoldas());
// // //##################################################
// //            
// //            
// //            
// //            
// //            
// //            
// //            
// //         }
// //      
// //
// //    }
// //}
