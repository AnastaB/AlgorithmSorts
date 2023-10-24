using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        int[] arr = GenerateRandomNumber(10, 0, 100);

        int depthLimit = 2 * (int)Math.Log(arr.Length);

        Console.WriteLine("1. Selection Sort");
        Console.WriteLine("2. Insertion Sort");
        Console.WriteLine("3. Bubble Sort");
        Console.WriteLine("4. Merge Sort");
        Console.WriteLine("5. Quick Sort");
        Console.WriteLine("6. Shell Sort");
        Console.WriteLine("7. Heap Sort");
        Console.WriteLine("8. Timsort");
        Console.WriteLine("9. Introsort");

        Console.Write("Enter a number to select a sorting method: ");
        int option = int.Parse(Console.ReadLine());

        Console.WriteLine("Array:");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }

        // Sort array using various algorithms
        switch (option)
        {
            case 1:
                SelectionSort(arr);
                var runningTime = MeasureRunningTime(() => SelectionSort(arr));
                Console.WriteLine("\nRunning time: " + runningTime + " ms");
                break;
            case 2:
                InsertionSort(arr, 0, arr.Length);
                runningTime = MeasureRunningTime(() => InsertionSort(arr, 0, arr.Length));
                Console.WriteLine("\nRunning time: " + runningTime + " ms");
                break;
            case 3:
                BubbleSort(arr);
                runningTime = MeasureRunningTime(() => BubbleSort(arr));
                Console.WriteLine("\nRunning time: " + runningTime + " ms");
                break;
            case 4:
                MergeSort(arr, 0, arr.Length - 1);
                runningTime = MeasureRunningTime(() => MergeSort(arr, 0, arr.Length - 1));
                Console.WriteLine("\nRunning time: " + runningTime + " ms");
                break;
            case 5:
                QuickSort(arr, 0, arr.Length - 1);
                runningTime = MeasureRunningTime(() => QuickSort(arr, 0, arr.Length - 1));
                Console.WriteLine("\nRunning time: " + runningTime + " ms");
                break;
            case 6:
                ShellSort(arr, new int[] { 1000, 500, 100, 50, 10, 1 });
                //ShellSort(arr, new int[] {100, 50, 25, 10, 1 });
                //ShellSort(arr, new int[] { 5, 3, 1 });
                runningTime = MeasureRunningTime(() => ShellSort(arr, new int[] { 1000, 500, 100, 50, 10, 1 }));
                Console.WriteLine("\nRunning time: " + runningTime + " ms");
                break;
            case 7:
                HeapSort(arr);
                runningTime = MeasureRunningTime(() => HeapSort(arr));
                Console.WriteLine("\nRunning time: " + runningTime + " ms");
                break;
            case 8:
                Timsort(arr);
                runningTime = MeasureRunningTime(() => Timsort(arr));
                Console.WriteLine("\nRunning time: " + runningTime + " ms");
                break;
            case 9:
                IntroSort(arr, 0, arr.Length - 1, depthLimit);
                runningTime = MeasureRunningTime(() => IntroSort(arr, 0, arr.Length - 1, depthLimit));
                Console.WriteLine("\nRunning time: " + runningTime + " ms");
                break;
            default:
                Console.WriteLine("Invalid option");
                break;
        }

        Console.WriteLine("\nSorted array:");
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }
    }

    static int[] GenerateRandomNumber(int size, int min, int max)
    {
        // Generate random numbers
        int[] array = new int[size];
        Random rand = new Random();
        for (int i = 0; i < size; i++)
        {
            array[i] = rand.Next(min, max + 1);
        }
        return array;
    }

    static void SelectionSort(int[] arr)
    {
        // Sorting by selection
        for (int i = 0; i < arr.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }
            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }
    }

    static void InsertionSort(int[] arr, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int key = arr[i];
            int j = i - 1;
            while (j >= left && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = key;
        }
    }

    static void BubbleSort(int[] arr)
    {
        // Bubble sort
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    static void MergeSort(int[] arr, int left, int right)
    {
        // Merge sort
        if (left < right)
        {
            int mid = (left + right) / 2;
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }

    static void Merge(int[] arr, int left, int mid, int right)
    {
        int[] temp = new int[right - left + 1];
        int i = left, j = mid + 1, k = 0;

        while (i <= mid && j <= right)
        {
            if (arr[i] <= arr[j])
            {
                temp[k] = arr[i];
                i++;
            }
            else
            {
                temp[k] = arr[j];
                j++;
            }
            k++;
        }

        while (i <= mid)
        {
           // if(i < k)
                temp[k] = arr[i];
            i++;
            k++;
        }

        while (j <= right)
        {
            temp[k] = arr[j];
            j++;
            k++;
        }

        for (int x = 0; x < temp.Length; x++)
        {
            arr[left + x] = temp[x];
        }
    }

    static void QuickSort(int[] arr, int left, int right)
    {
        // Quick sort
        if (left < right)
        {
            int pivotIndex = Partition(arr, left, right);
            QuickSort(arr, left, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, right);
        }
    }

    static int Partition(int[] arr, int left, int right)
    {
        int pivot = arr[right];
        int i = left - 1;
        for (int j = left; j < right; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        int temp2 = arr[i + 1];
        arr[i + 1] = arr[right];
        arr[right] = temp2;
        return i + 1;
    }

    static void ShellSort(int[] arr, int[] gaps)
    {
        // Shell sort
        foreach (int gap in gaps)
        {
            for (int i = gap; i < arr.Length; i++)
            {
                int temp = arr[i];
                int j;
                for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                {
                    arr[j] = arr[j - gap];
                }
                arr[j] = temp;
            }
        }
    }

    static void HeapSort(int[] arr)
    {
        // Pyramid sort
        for (int i = arr.Length / 2 - 1; i >= 0; i--)
        {
            Heapify(arr, arr.Length, i);
        }

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            Heapify(arr, i, 0);
        }
    }

    static void Heapify(int[] arr, int n, int i)
    {
        int largest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < n && arr[l] > arr[largest])
        {
            largest = l;
        }

        if (r < n && arr[r] > arr[largest])
        {
            largest = r;
        }

        if (largest != i)
        {
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;

            Heapify(arr, n, largest);
        }
    }

    static void IntroSort(int[] arr, int left, int right, int depthLimit)
    {
        // IntroSort
        if (left < right)
        {
            // Check if depth limit has been reached
            if (depthLimit == 0)
            {
                // If depth limit has been reached, switch to HeapSort
                HeapSort(arr, left, right);
            }
            else
            {
                // Partition the array using the last element as the pivot
                int pivotIndex = Partition(arr, left, right);

                // Recursively sort the left and right partitions
                IntroSort(arr, left, pivotIndex - 1, depthLimit - 1);
                IntroSort(arr, pivotIndex + 1, right, depthLimit - 1);
            }
        }
    }

    static void HeapSort(int[] arr, int left, int right)
    {
        // HeapSort
        for (int i = right / 2 - 1; i >= left; i--)
        {
            Heapify(arr, right - left + 1, i, left);
        }

        for (int i = right; i >= left; i--)
        {
            int temp = arr[left];
            arr[left] = arr[i];
            arr[i] = temp;

            Heapify(arr, i - left, 0, left);
        }
    }

    static void Heapify(int[] arr, int n, int i, int offset)
    {
        int largest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < n && arr[l + offset] > arr[largest + offset])
        {
            largest = l;
        }

        if (r < n && arr[r + offset] > arr[largest + offset])
        {
            largest = r;
        }

        if (largest != i)
        {
            int temp = arr[i + offset];
            arr[i + offset] = arr[largest + offset];
            arr[largest + offset] = temp;

            Heapify(arr, n, largest, offset);
        }
    }

    static void Timsort(int[] arr)
    {
        // Define minimum run size
        int minRun = 32;

        // Divide array into runs
        for (int i = 0; i < arr.Length; i += minRun)
        {
            int end = Math.Min(i + minRun - 1, arr.Length - 1);
            InsertionSort(arr, i, end);
        }

        // Merge adjacent runs
        for (int size = minRun; size < arr.Length; size *= 2)
        {
            for (int left = 0; left < arr.Length; left += 2 * size)
            {
                int mid = left + size - 1;
                int right = Math.Min(left + 2 * size - 1, arr.Length - 1);
                Merge(arr, left, mid, right);
            }
        }
    }

    public static double MeasureRunningTime(Action method)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        method();
        stopwatch.Stop();
        return (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
    }

}
