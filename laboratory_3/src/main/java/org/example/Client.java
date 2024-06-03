package org.example;
import java.io.*;
import java.net.*;

public class Client {
    public static void main(String[] args) {
        try (Socket socket = new Socket("localhost", 5000);
             ObjectOutputStream out = new ObjectOutputStream(socket.getOutputStream());
             ObjectInputStream in = new ObjectInputStream(socket.getInputStream());
             BufferedReader userInput = new BufferedReader(new InputStreamReader(System.in))) {

            in.readObject();
            System.out.println("Enter an integer value:");
            int n = Integer.parseInt(userInput.readLine());
            out.writeObject(n);
            in.readObject();

            System.out.println("Enter a string:");
            String string = userInput.readLine();
            for (int i = 1; i <= n; i++) {
                Message message = new Message(i, string + ' ' + i);
                out.writeObject(message);
            }
            in.readObject();
        } catch (IOException | NumberFormatException e) {
            e.printStackTrace();
        } catch (ClassNotFoundException e) {
            throw new RuntimeException(e);
        }
    }
}