package org.example;

import java.util.Queue;
import static java.lang.Math.sqrt;

public class Task_Processor implements Runnable {
    private Task_Queue queue;
    private Result_Collector resultCollector;

    public Task_Processor(Task_Queue taskQueue, Result_Collector resultCollector) {
        this.queue = taskQueue;
        this.resultCollector = resultCollector;
    }

    public Result_Collector getResultCollector() {
        return resultCollector;
    }

    public Task_Queue getQueue() {
        return queue;
    }

    @Override
    public void run() {
        while (true) {
            try {
                Task task = queue.getTask();
                boolean result = task.check_if_number_is_prime();
                resultCollector.addResult(task, result);
                System.out.println("Number: " + task.getNumber() + ", is the number prime: " + result);
                // resultCollector.printResults();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}
