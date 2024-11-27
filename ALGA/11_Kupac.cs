namespace OE.ALGA.Adatszerkezetek;

public class Kupac<T>
{
    // Protected fields
    protected T[] E;
    protected int n;
    protected Func<T, T, bool> nagyobbPrioritas;

    // Constructor
    public Kupac(T[] input, int countElem, Func<T, T, bool> nagyobbPrioritas)
    {
        E = input;
        n = countElem;
        this.nagyobbPrioritas = nagyobbPrioritas;
        KupacotEpit();
    }

    // Static methods for tree navigation
    public static int Bal(int i) => 2 * i + 1;
    public static int Jobb(int i) => 2 * i + 2;
    public static int Szulo(int i) => (i - 1) / 2;

    // Heapify a node at index i
    public void Kupacol(int i)
    {
        int bal = Bal(i), jobb = Jobb(i), max = i;

        // Check left child
        if (bal < n && nagyobbPrioritas(E[bal], E[max]))
        {
            max = bal;
        }

        // Check right child
        if (jobb < n && nagyobbPrioritas(E[jobb], E[max]))
        {
            max = jobb;
        }

        // If max is not the current node, swap and recurse
        if (max != i)
        {
            var temp = E[i];
            E[i] = E[max];
            E[max] = temp;
            Kupacol(max);
        }
    }

    // Build the heap
    public void KupacotEpit()
    {
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Kupacol(i);
        }
    }
}
//########################################################################
// ########################───▄▄─▄████▄▐▄▄▄▌##############################
// ########################──▐──████▀███▄█▄▌##############################
// ########################▐─▌──█▀▌──▐▀▌▀█▀ ##############################
// ########################─▀───▌─▌──▐─▌    ##############################
// ########################─────█─█──▐▌█    ##############################
//########################################################################
public class KupacRendezes<T> : Kupac<T> where T : IComparable<T>
{
    // Constructor
    public KupacRendezes(T[] E) : base(E, E.Length, (x, y) => x.CompareTo(y) > 0)
    {
    }

    // Sorting method
    public void Rendezes()
    {
        KupacotEpit(); // Build the heap
        for (int i = n - 1; i > 0; i--)
        {
            // Swap the root with the last element
            var temp = E[0];
            E[0] = E[i];
            E[i] = temp;

            // Reduce the heap size and re-heapify
            n--;
            Kupacol(0);
        }
    }
}
//########################################################################
//──────▄▀▄─────▄▀▄
//─────▄█░░▀▀▀▀▀░░█▄
//─▄▄──█░░░░░░░░░░░█──▄▄
//█▄▄█─█░░▀░░┬░░▀░░█─█▄▄█
//########################################################################
public class KupacPrioritasosSor<T> : Kupac<T>, PrioritasosSor<T>
{
    // Constructor
    public KupacPrioritasosSor(int meret, Func<T, T, bool> nagyobbPrioritas)
        : base(new T[meret], 0, nagyobbPrioritas)
    {
    }

    // Move a key upward in the heap
    private void KulcsotFelvisz(int i)
    {
        int sz = Szulo(i);

        if (sz >= 0 && nagyobbPrioritas(E[i], E[sz]))
        {
            var temp = E[sz];
            E[sz] = E[i];
            E[i] = temp;
            KulcsotFelvisz(sz);
        }
    }

    // Check if the queue is empty
    public bool Ures => n == 0;

    // Insert a value into the queue
    public void Sorba(T ertek)
    {
        if (n < E.Length)
        {
            E[n] = ertek;
            KulcsotFelvisz(n);
            n++;
        }
        else throw new NincsHelyKivetel();
    }

    // Remove and return the highest-priority element
    public T Sorbol()
    {
        if (!Ures)
        {
            var max = E[0];
            E[0] = E[n - 1];
            n--;
            Kupacol(0);
            return max;
        }
        else throw new NincsElemKivetel();
    }

    // Get the highest-priority element without removing it
    public T Elso()
    {
        if (!Ures) return E[0];
        throw new NincsElemKivetel();
    }

    // Update a specific element in the heap
    public void Frissit(T elem)
    {
        int i = 0;
        while (i < n && !E[i].Equals(elem))
        {
            i++;
        }

        if (i < n)
        {
            KulcsotFelvisz(i);
            Kupacol(i);
        }
        else throw new NincsElemKivetel();
    }
}
    
    
    


