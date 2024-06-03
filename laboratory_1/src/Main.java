import java.util.*;

public class Main {
    public static void main(String[] args) {
        String sortingType = args[0];

        Set<Mage> set;
        Map<Mage, Integer> map;
        // Wybór sposobu sortowania na podstawie parametru startowego
        if (sortingType.equals("natural")) {
            set = new TreeSet<>();
            map = new TreeMap<>();
        } else if (sortingType.equals("alternative")) {
            set = new TreeSet<>(new Mage.CustomComparator());
            map = new TreeMap<>(new Mage.CustomComparator());
        } else {
            set = new HashSet<>();
            map = new HashMap<>();
        }

        Mage mag_1_1 = new Mage(200, 200, "Superior Magus");
        Mage mag_1_2 = new Mage(200, 200, "Magus Maximus");
        Mage mag_1_3 = new Mage(100, 500, "Albus Dumbledore");
        Mage mag_2_1 = new Mage(50, 90, "Apprentice Magus 1");
        Mage mag_2_2 = new Mage(50, 60, "Apprentice Magus 2");
        Mage mag_2_3 = new Mage(50, 90, "Apprentice Magus 3");
        Mage mag_2_4 = new Mage(45, 60, "Apprentice Magus 4");
        Mage mag_3_1 = new Mage(10, 15, "Studentus Magus 1");
        Mage mag_3_2 = new Mage(6, 20, "Studentus Magus 2");
        Mage mag_3_3 = new Mage(8, 15, "Studentus Magus 3");
        Mage mag_3_4 = new Mage(12, 10, "Studentus Magus 4");
        Mage mag_3_5 = new Mage(14, 15, "Studentus Magus 5");
        mag_1_1.addApprentice(mag_2_1);
        mag_1_1.addApprentice(mag_2_2);
        mag_1_2.addApprentice(mag_2_3);
        mag_1_3.addApprentice(mag_2_4);
        mag_2_1.addApprentice(mag_3_1);
        mag_2_1.addApprentice(mag_3_2);
        mag_2_2.addApprentice(mag_3_3);
        mag_2_3.addApprentice(mag_3_4);
        mag_2_3.addApprentice(mag_3_5);

        Mage[] allMages = {mag_1_1, mag_1_2, mag_1_3};

        // Dodawanie magów do zbioru
        for (Mage mage : allMages) {
            set.add(mage);
            mage.update_map(map);
        }

        // Wypisywanie zbioru
        for (Mage element : set) {
            System.out.println(element.toString());
        }
        System.out.println("\n\nMap:\n");
        for(Map.Entry<Mage, Integer> element : map.entrySet()){
            System.out.println(element.getKey() + "ma " + element.getValue() + " uczniów");
        }
    }
}