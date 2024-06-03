package org.example;

import java.util.LinkedList;
import java.util.Queue;

public class Task_Queue
{
    private Queue<Task> tasks = new LinkedList<>();

    public synchronized void addTask(Task task) {
        tasks.add(task);
        notify();
    }

    public synchronized Task getTask() throws InterruptedException {
        while (tasks.isEmpty()) {
            wait();
        }
        return tasks.remove();
    }
}