package org.example;

import java.io.*;
import java.net.*;

public class Server {
    public static void main(String[] args) {
        try (ServerSocket serverSocket = new ServerSocket(5000)) {
            System.out.println("Server started");

            while (true) {
                Socket socket = serverSocket.accept();
                System.out.println("Client connected");

                new ServerThread(socket).start();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

class ServerThread extends Thread {
    private Socket socket;

    public ServerThread(Socket socket) {
        this.socket = socket;
    }

    public void run() {
        try (ObjectOutputStream out = new ObjectOutputStream(socket.getOutputStream());
             ObjectInputStream in = new ObjectInputStream(socket.getInputStream())) {

            System.out.println("ready");
            out.writeObject("ready");
            Object obj = in.readObject();
            if (obj instanceof Integer) {
                int n = (Integer) obj;
                System.out.println("Server received number: " + n);
                System.out.println("ready for messages");
                out.writeObject("ready for messages");
                for (int i = 0; i < n; i++) {
                    Message message = (Message) in.readObject();
                    System.out.println("Message received: " + message.getContent());
                }
                System.out.println("finished");
                out.writeObject("finished");
            }
        } catch (IOException | ClassNotFoundException e) {
            e.printStackTrace();
        }
    }
}
