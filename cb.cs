using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;

namespace BASICsharp {
    public class BASICsharp {
            static double mathresult = 0;
			static string[] memory = {"", "", "", "", "", "", "", "", "", ""};
			static List<string> commands = new List<string>();
            static bool done = false;
            static bool fileopened = false;
			static string inputresult = "";
            static string openfile = "";
		static double EvaluateExpression(string expression)
    	{
        	char[] separators = { '+', '-', '*', '/' };
        	string[] parts = expression.Split(separators);

        	double operand1 = Convert.ToDouble(parts[0].Trim());
        	double operand2 = Convert.ToDouble(parts[1].Trim());
        	char operation = expression[parts[0].Length];

        	switch (operation)
        	{
        	    case '+':
        	        return operand1 + operand2;
        	    case '-':
        	        return operand1 - operand2;
        	    case '*':
        	        return operand1 * operand2;
        	    case '/':
        	        if (operand2 == 0)
        	        {
        	            throw new DivideByZeroException("Cannot divide by zero.");
        	        }
        	        return operand1 / operand2;
        	    default:
        	        throw new ArgumentException("Invalid operation.");
        	}
    }

	static void RunCommand(string[] commands, int linenum) {
								foreach (string line in commands) {
							if (line.StartsWith("input")) {
								if (line.Length > 6) {
									if (line.Substring(6).StartsWith("$")) {
										int index = int.Parse(line.Substring(7, 2));
										Console.Write(memory[index] + "> ");
									} else {
										Console.Write(line.Substring(6) + "> ");
									}
								} else {
									Console.WriteLine("input>");
								}
								inputresult = Console.ReadLine();
							} else if (line.StartsWith("print") && line.Length > 6) {
								if (line.Substring(6) == "!inputresult!") {
									Console.WriteLine(inputresult);
								} else if (line.Substring(6) == "!mathresult!") {
									Console.WriteLine(mathresult);
								} else if (line.Substring(6).StartsWith("$")) {
									int index = int.Parse(line.Substring(7, 2));
									if (index <= 10 && index >= 1) {
										Console.WriteLine(memory[index]);
									}
								} else {
									Console.WriteLine(line.Substring(6));
								}
							} else if (line == "clear") {
								Console.Clear();
							} else if (line.StartsWith("wait") && line.Length > 5) {
								int waittime;
								if (line.Substring(5).StartsWith("$")) { 
									int index = int.Parse(line.Substring(6, 2));
									waittime = int.Parse(memory[index]);
								} else {
									waittime = int.Parse(line.Substring(5));
								}
								Thread.Sleep(waittime * 1000);
							} else if (line.StartsWith("math") && line.Length > 5) {
								double equation = 0;
								int index = int.Parse(line.Substring(6, 2));
								equation = EvaluateExpression(memory[index]);
								mathresult = equation;
							} else if (line.StartsWith("--")) {
								continue;
							} else if (line.StartsWith("$") && line.Length >= 3) {
								if (line.Length == 3 && line.Length < 4) {
									int index = int.Parse(line.Substring(1, 2));
									if (index <= 10 && index >= 1) {
										Console.WriteLine(memory[index]);
									}
								} else if (line.Length >= 4 && line.Substring(4,1) == "=") {
									int index = int.Parse(line.Substring(1,2));
									string value = line.Substring(4);
									if (value == "!inputresult!") {
										memory[index] = Convert.ToString(inputresult);
									} else if (value == "!mathresult!") {
										memory[index] = Convert.ToString(mathresult);
									} else {
										memory[index] = Convert.ToString(value);
									}
								}
							} else {
								Console.WriteLine("Error: command on line #" + linenum.ToString() + " not recognized or is incomplete (2). Halting.");
								break;
							}
							linenum += 1;
						}
	}
	static void CLI() {
            while(done == false) {
                Console.Write("C#ASIC>");
                string basicsharpinput = Console.ReadLine();
                if (basicsharpinput == "help") {
                    Console.WriteLine("help - Displays this block of text");
                    Console.WriteLine("new <FILE> - Starts a new program");
                    Console.WriteLine("run - Runs a program");
                    Console.WriteLine("com - Writes a command to open file");
                    Console.WriteLine("end - Ends file editing");
                    Console.WriteLine("list - Shows all lines of code in open file");
                    Console.WriteLine("ls - Lists all programs made with C#ASIC");
                    Console.WriteLine("del - Deletes a program.");
                    Console.WriteLine("open - Just like new, but instead of creating a file it just opens it (hence the name).");
                    Console.WriteLine("exit - Exits C#ASIC CLI");
                } else if (basicsharpinput == "new") {
					Console.Write("Enter name of program: ");
					string name = Console.ReadLine();
                    if (name == "") {
                        Console.WriteLine("argument 'FILE' must not be empty");
                    } else {
                        openfile = name;
						commands.Clear();
                        File.Create(@"./" + name.ToUpper() + ".cb").Dispose();
                        fileopened = true;
                    }
				} else if (basicsharpinput == "exit") {
					fileopened = false;
					done = true;
					Environment.Exit(0);
				} else if (basicsharpinput == "end") {
					if (openfile != null && fileopened == true) {
							Console.WriteLine("Ending editing of file '" + openfile.ToString() + "'");
						try {
							using (StreamWriter w = File.AppendText(@"./" + openfile.ToUpper() + ".cb")) {
								foreach (string command in commands.ToArray()) {
									w.WriteLine(command);
								}
							}
						} catch (IOException e) {
							Console.WriteLine(e);
						}
						openfile = "";
						commands.Clear();
						fileopened = false;
					} else {
						Console.WriteLine("No file currently open");
					}
				} else if (basicsharpinput == "com") {
					if (fileopened == false) {
						Console.WriteLine("No file currently open");
					} else {
											Console.Write("Enter command: ");
					string code = Console.ReadLine();
					if (code != null) {
						commands.Add(code);
					} else {
						Console.WriteLine("argument <COMMAND> must not be empty");
					}
					}
				} else if (basicsharpinput == "ls") {
					string[] dir = Directory.GetFiles(".");
					Console.WriteLine("List of C#ASIC programs in this directory:");
					foreach (string literalFile in dir) {
						string file = literalFile.ToString();
						if (file.EndsWith(".cb")) {
							file = file.Substring(2, file.Length-5);
							Console.WriteLine(file);
						} else {
							
						}
					}
				} else if (basicsharpinput == "del") {
					Console.Write("Enter program to delete: ");
					string prgm = Console.ReadLine();
					if (File.Exists("./" + prgm.ToUpper() + ".cb")) {
						if (fileopened == true && openfile == prgm) {
							fileopened = false;
							Console.WriteLine("Deleting " + prgm.ToUpper() + "...");
							File.Delete("./" + prgm.ToUpper() + ".cb");
							Console.WriteLine("Deleted program " + prgm.ToUpper());
						} else {
							Console.WriteLine("Deleting " + prgm.ToUpper() + "...");
							File.Delete("./" + prgm.ToUpper() + ".cb");
							Console.WriteLine("Deleted program " + prgm.ToUpper());
						}
					} else {
						Console.WriteLine("Program not found.");
					}
				} else if (basicsharpinput == "run") { 
					if (openfile != null && fileopened == true) {
						string[] contents = commands.ToArray();
						int linenum = 1;
						RunCommand(contents, linenum);
					} else {
						Console.WriteLine("No currently open file");
					}
				} else if (basicsharpinput == "list") {
					if (fileopened == true) {
						foreach (string lineofcode in commands.ToArray()) {
							Console.WriteLine(lineofcode);
						}
					} else {
						Console.WriteLine("No file currently open");
					}
				} else if (basicsharpinput == "open") {
					Console.Write("Enter name of program: ");
					string name = Console.ReadLine();
                    if (name == "") {
                        Console.WriteLine("argument 'FILE' must not be empty");
                    } else {
						commands.Clear();
						if (File.Exists("./" + name + ".cb")) {
                        	openfile = name;
							foreach (string line in File.ReadAllLines("./" + name + ".cb")) {
								commands.Add(line);
							}
                        	fileopened = true;
						} else {
							Console.WriteLine("argument <FILE> does not exist");
						}
                    }
				} else if (basicsharpinput == "clear") {
					Console.Clear();
				} else if (basicsharpinput.StartsWith("$") && basicsharpinput.Length >= 3) {
								if (basicsharpinput.Length == 3) {
									int index = int.Parse(basicsharpinput.Substring(1, 2));
									if (index <= 10 && index >= 1) {
										Console.WriteLine(memory[index]);
									}
								} else if (basicsharpinput.Length >= 4 && basicsharpinput.Substring(3,1) == "=") {
									int index = int.Parse(basicsharpinput.Substring(1,2));
									string value = basicsharpinput.Substring(4);
									memory[index] = value;
								}
				} else {
					Console.WriteLine("Error: command '" + basicsharpinput + "' not recognized (1).");
				}
			}
	}
        public static void Main(string[] args) {
			string[] arguments = Environment.GetCommandLineArgs();
			if (arguments.Length == 1) {
				Console.WriteLine("cb: No .cb program specified");
		} else {
			if (arguments[1] == "cli") {
				CLI();
			} else {
			if (File.Exists(arguments[1])) {
				if (arguments[1].EndsWith(".cb")) {
					string[] file = File.ReadAllLines(arguments[1]);
					int linenum = 0;
					RunCommand(file, linenum);
				} else {
					Console.WriteLine("Error: program is using legacy .bsharp extension. Please convert and try again (5).");
				}
			} else {
				Console.WriteLine("Error: program doesn't exist (4).");
			}
		}
			}
		}
	}
}
