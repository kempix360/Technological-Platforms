package org.example;

import static java.lang.Math.sqrt;

public class Task
{
    private final int number;

    public Task(int number) {
        this.number = number;
    }

    public int getNumber() {
        return number;
    }

    public boolean check_if_number_is_prime(){
        if (this.number<=0)
            return false;
        if (this.number == 2)
            return true;
        if (this.number%2==0)
            return false;
        for (int i=2;i<=sqrt(this.number);i++){
            if (this.number%i==0) {
                try {
                    // Symulacja złożoności obliczeń poprzez oczekiwanie losowego czasu
                    Thread.sleep(number/1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                return false;
            }
        }
        return true;
    }

}
