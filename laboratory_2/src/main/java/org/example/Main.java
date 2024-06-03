package org.example;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Result_Collector result_collector = new Result_Collector();
        Task_Queue taskQueue = new Task_Queue();
        Thread[] array = new Thread[3];
        for (int i = 0; i < 3; i++) {
            Thread thread = new Thread(new Task_Processor(taskQueue, result_collector));
            array[i] = thread;
            thread.start();
        }

        Scanner scanner = new Scanner(System.in);
        while (true) {
            System.out.println("Podaj liczbę lub 'x', aby zakończyć: ");
            String input = scanner.nextLine();
            if (input.equals("x")) {
                result_collector.printResults();
                break;
            }
            else {
                try {
                    int number = Integer.parseInt(input);
                    taskQueue.addTask(new Task(number));
                }
                catch (NumberFormatException e) {
                    System.out.println("Podana liczba jest nieprawidłowa.");
                }
            }
        }
        for (Thread thread : array){
            thread.interrupt();
        }
    }
}
