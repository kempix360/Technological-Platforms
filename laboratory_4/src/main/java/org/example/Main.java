package org.example;

import java.util.Scanner;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.query.Query;

import java.util.ArrayList;
import java.util.List;

public class Main {
    public static void main(String[] args) {
        SessionFactory sessionFactory = HibernateUtil.getSessionFactory();
        Session session = sessionFactory.openSession();
        try {
            removeAllMages(session);
            removeAllTowers(session);

            printData(session);
            creatingTestSet(session);
            printData(session);
            removeMage("Cho Chang", session);
            printData(session);

            Query<Mage> query = session.createQuery(
                    "FROM Mage m WHERE m.level > :level AND m.tower.name = :towerName",
                    Mage.class
            );
            query.setParameter("level", 70);
            query.setParameter("towerName", "Gryffindor");
            List<Mage> mages = query.getResultList();

            System.out.println("Query results:");
            for (Mage mage : mages) {
                System.out.println("Mage: " + mage.getName() + ", Level: " + mage.getLevel());
            }

            waitForTheInput(session);

        } catch (Exception e) {
            System.out.println("Found an exception!");
            e.printStackTrace();
        } finally {
            if (session != null && session.isOpen()) {
                System.out.println("The database session was closed.");
                session.close();
            }
        }
    }


    public static void creatingTestSet(Session session) {
        Tower ravenclaw = new Tower("Ravenclaw", 80, new ArrayList<>());
        Tower gryffindor = new Tower("Gryffindor", 100, new ArrayList<>());
        Tower slytherin = new Tower("Slytherin", 90, new ArrayList<>());
        Mage mage1 = new Mage("Harry Potter", 80, gryffindor);
        Mage mage2 = new Mage("Draco Malfoy", 80, slytherin);
        Mage mage3 = new Mage("Hermione Granger", 99, gryffindor);
        Mage mage4 = new Mage("Ronald Weasley", 60, gryffindor);
        Mage mage5 = new Mage("Cho Chang", 65, ravenclaw);
        Mage mage6 = new Mage("Cedric Diggory", 60, slytherin);
        Mage mage7 = new Mage("Luna Lovegood", 50, ravenclaw);
        Mage mage8 = new Mage("Ginny Weasley", 60, ravenclaw);

        addTower(ravenclaw, session);
        addTower(gryffindor, session);
        addTower(slytherin, session);
        addMage(mage1, session);
        addMage(mage2, session);
        addMage(mage3, session);
        addMage(mage4, session);
        addMage(mage5, session);
        addMage(mage6, session);
        addMage(mage7, session);
        addMage(mage8, session);

    }

    public static void addMage(Mage mage, Session session) {
        session.beginTransaction();

        Query<Tower> query = session.createQuery("FROM Tower WHERE name = :name", Tower.class);
        query.setParameter("name", mage.getTower().getName());
        Tower tower = query.uniqueResult();

        if (tower == null) {
            System.out.println("Tower not found in the database");
        } else {
            session.persist(mage);
            tower.addMage(mage);
            session.persist(tower);
            System.out.println("Mage " + mage.getName() + " was added to the database.");
        }
        session.getTransaction().commit();
    }

    public static void addTower(Tower tower, Session session) {
        session.beginTransaction();
        session.persist(tower);
        System.out.println("Tower " + tower.getName() + " was added to the database.");
        session.getTransaction().commit();
    }

    public static void removeMage(String name, Session session) {
        session.beginTransaction();
        Query query = session.createQuery("from Mage where name = :name");
        query.setParameter("name", name);
        Mage mage = (Mage) query.uniqueResult();

        if (mage != null) {
            Tower tower = mage.getTower();
            tower.getMages().remove(mage);
            session.persist(tower);
            session.remove(mage);
            System.out.println("Mage " + mage.getName() + " was removed from the database.");
        }
        else{
            System.out.println("Mage not found in the database");
        }
        session.getTransaction().commit();
    }

    public static void removeTower(String name, Session session) {
        Query<Tower> query = session.createQuery("FROM Tower WHERE name = :name", Tower.class);
        query.setParameter("name", name);
        Tower tower = query.uniqueResult();

        if (tower == null) {
            System.out.println("Tower not found in the database");
        }
        else{
            List<Mage> magesToDelete = tower.getMages();
            int mages_size = magesToDelete.size();
            while (mages_size > 0) {
                Mage mage = magesToDelete.get(mages_size-1);
                removeMage(mage.getName(), session);
                mages_size -= 1;
            }
            session.remove(tower);
            System.out.println("Tower " + tower.getName() + " and all its mages were removed from the database.");

        }
    }

    public static void printData(Session session) {
        session.beginTransaction();

        List<Tower> towers = session.createQuery("from Tower", Tower.class).getResultList();
        System.out.println("\n------------------------");
        for (Tower t : towers) {
            System.out.println("Tower: " + t.getName());
            for (Mage m : t.getMages()) {
                System.out.println("    Mage: " + m.getName() + ", Level: " + m.getLevel());
            }
        }
        System.out.println("------------------------");
        session.getTransaction().commit();
    }

    public static void removeAllMages(Session session) {
        Query<String> query = session.createQuery("select name from Mage", String.class);
        List<String> mageNames = query.getResultList();

        for (String name : mageNames) {
            removeMage(name, session);
        }
    }

    public static void removeAllTowers(Session session) {
        List<Tower> towers = session.createQuery("from Tower", Tower.class).getResultList();
        for (Tower t : towers) {
            removeTower(t.getName(), session);
        }
    }

    public static void waitForTheInput(Session session){
        Scanner scanner = new Scanner(System.in);
        boolean exit = false;
        while (!exit) {
            System.out.println("\nChoose an option:");
            System.out.println("1. Add Tower");
            System.out.println("2. Add Mage");
            System.out.println("3. Delete Tower");
            System.out.println("4. Delete Mage");
            System.out.println("5. Print all data");
            System.out.println("6. Exit");

            int option;
            try {
                option = Integer.parseInt(scanner.nextLine());
            } catch (NumberFormatException e) {
                System.out.println("Invalid input. Please enter a number.");
                continue;
            }

            switch (option) {
                case 1:
                    System.out.println("Enter the tower name:");
                    String name = scanner.nextLine();
                    System.out.println("Enter the tower height:");
                    int height = Integer.parseInt(scanner.nextLine());
                    Tower tower = new Tower(name, height, new ArrayList<>());
                    addTower(tower, session);
                    break;
                case 2:
                    System.out.println("Enter the mage name:");
                    String mage_name = scanner.nextLine();
                    System.out.println("Enter the mage level:");
                    int level = Integer.parseInt(scanner.nextLine());
                    System.out.println("Enter the tower name:");
                    String tower_name = scanner.nextLine();

                    Query<Tower> query = session.createQuery("FROM Tower WHERE name = :name", Tower.class);
                    query.setParameter("name", tower_name);
                    Tower tower1 = query.uniqueResult();

                    Mage mage = new Mage(mage_name, level, tower1);
                    addMage(mage, session);
                    break;
                case 3:
                    System.out.println("Enter the tower name:");
                    String tower_name_1 = scanner.nextLine();
                    removeTower(tower_name_1, session);
                    break;
                case 4:
                    System.out.println("Enter the mage name:");
                    String mage_name_1 = scanner.nextLine();
                    removeMage(mage_name_1, session);
                    break;
                case 5:
                    printData(session);
                    break;
                case 6:
                    exit = true;
                    break;
                default:
                    System.out.println("Invalid option number.");
            }
        }

        scanner.close();
    }
}
