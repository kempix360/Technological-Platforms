package org.example;

import java.util.LinkedHashMap;
import java.util.Map;

public class Result_Collector {
    private final Map<Task, Boolean> resultMap;

    public Result_Collector() {
        this.resultMap = new LinkedHashMap<>();
    }

    public synchronized void addResult(Task number, boolean result) {
        resultMap.put(number, result);
    }

    public synchronized void printResults() {
        System.out.println("Wyniki:");
        for (Map.Entry<Task, Boolean> entry : resultMap.entrySet()) {
            System.out.println("Number: " + entry.getKey().getNumber() + ", is the number prime: " + entry.getValue());
        }
    }
}