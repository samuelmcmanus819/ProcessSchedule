using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ProcessSchedule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string fileContent;
            fileContent = FileFunctions.ReadFile();
            string[] input = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] values = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                values[i] = int.Parse(input[i]);
            }

            Process[] processes = new Process[values[0]];
            for (int i = 0; i < values[0]; i++)
            {
                processes[i] = new Process
                {
                    processNum = i
                };
                processes[i].startTime =
                    TimeSpan.FromSeconds(values[((i * 3) + 1)]);
                processes[i].priority = values[((i * 3) + 2)];
                processes[i].duration =
                    TimeSpan.FromSeconds(values[((i * 3) + 3)]);

            }

            Process currentProc, oldProc;
            List<Process> samePrioProcesses = new List<Process>
            {

            };
            int index, runtime, curProcIndex;
            TimeSpan time;
            time = TimeSpan.FromSeconds(0);
            oldProc = processes[0];
            currentProc = oldProc;

            index = processes.Length;
            List<Process> processList = new List<Process>
            {

            };
            for (int i = 0; i < index; i++)
            {
                processList.Add(processes[i]);
            }

            runtime = 0;
            curProcIndex = 0;
            while (true)
            {
                samePrioProcesses = new List<Process>
                {

                };

                Console.WriteLine(time);
                if (processList.Count > 1)
                {
                    foreach (Process p in processList)
                    {
                        if ((p.startTime <= time) && (p.duration > TimeSpan.FromSeconds(0)))
                        {
                            if (p.priority < currentProc.priority)
                            {
                                currentProc = p;
                                samePrioProcesses = new List<Process>
                                {

                                };
                            }
                            else if ((p.priority == currentProc.priority) && (samePrioProcesses == null))
                            {
                                samePrioProcesses = new List<Process>
                                {
                                    currentProc,
                                    p
                                };
                            }
                            else if (p.priority == currentProc.priority)
                            {
                                samePrioProcesses.Add(p);
                            }
                        }
                    }

                    if (oldProc == currentProc)
                    {
                        runtime++;
                    }

                    if ((samePrioProcesses.Contains(currentProc)) && (runtime >= 2))
                    {
                        curProcIndex = samePrioProcesses.IndexOf(currentProc);
                        if (curProcIndex == samePrioProcesses.Count - 1)
                        {
                            curProcIndex = 0;
                        }
                        else
                        {
                            curProcIndex++;
                        }
                        runtime = 0;
                        currentProc = samePrioProcesses[curProcIndex];
                    }
                    currentProc.duration -= TimeSpan.FromSeconds(1);
                    time += TimeSpan.FromSeconds(1);
                    Console.WriteLine("Process " + currentProc.processNum);
                    Thread.Sleep(1000);

                    oldProc = currentProc;
                    if (currentProc.duration == TimeSpan.FromSeconds(0))
                    {
                        Console.WriteLine("Process " + currentProc.processNum + " finished");
                        processList.Remove(currentProc);
                        currentProc = processList[0];
                    }
                }
                else if(processList[0].duration > TimeSpan.FromSeconds(0))
                {
                    currentProc = processList[0];
                    Thread.Sleep(1000);
                    currentProc.duration -= TimeSpan.FromSeconds(1);
                    time += TimeSpan.FromSeconds(1);
                    Console.WriteLine("Process " + currentProc.processNum);
                }
                else
                {
                    Console.WriteLine("Finished");
                    break;
                }
            }
        }
    }
}
