import java.lang.management.GarbageCollectorMXBean;
import java.util.Comparator;
import java.util.HashSet;
import java.util.Objects;
import java.util.Set;
import java.util.Map;

class Mage implements Comparable<Mage> {
    private int level;
    private double power;
    private String name;
    Set<Mage> apprentices;

    public Mage(int level, double power, String name) {
        this.level = level;
        this.power = power;
        this.name = name;
        this.apprentices = new HashSet<>();
    }

    public void addApprentice(Mage apprentice) {
        apprentices.add(apprentice);
    }

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public double getPower() {
        return power;
    }

    public void setPower(double power) {
        this.power = power;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Set<Mage> getApprentices() {
        return apprentices;
    }

    public void setApprentices(Set<Mage> apprentices) {
        this.apprentices = apprentices;
    }

    @Override
    public int compareTo(Mage other) {
        if (!this.name.equals(other.name)) {
            return this.name.compareTo(other.name);
        } else if (this.level != other.level) {
            return Integer.compare(this.level, other.level);
        } else {
            return Double.compare(this.power, other.power);
        }
    }

    @Override
    public String toString(){
        return toString_second(this, 1);
    }

    public String toString_second(Mage mage, int stage) {
        StringBuilder sb = new StringBuilder();
        for (int i =0;i<stage-1;i++){
            sb.append(' ');
        }
        for (int i =0;i<stage;i++){
            sb.append('-');
        }
        sb.append("Mage{name='").append(mage.name)
                .append("', level=").append(mage.level)
                .append(", power=").append(mage.power)
                .append("}\n");

        for (Mage apprentice : mage.apprentices) {
            sb.append(toString_second(apprentice, stage + 1));
        }
        return sb.toString();
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (obj == null || getClass() != obj.getClass()) return false;
        Mage that = (Mage) obj;
        return level == that.level &&
                Double.compare(that.power, power) == 0 &&
                name.equals(that.name) &&
                Objects.equals(apprentices, that.apprentices);
    }

    @Override
    public int hashCode() {
        return Objects.hash(level, power, name, apprentices);
    }

    public static class CustomComparator implements Comparator<Mage> {
        @Override
        public int compare(Mage o1, Mage o2) {
            if (o1.level != o2.level) {
                return Integer.compare(o2.level, o1.level);
            } else if (!o1.name.equals(o2.name)) {
                return o1.name.compareTo(o2.name);
            } else {
                return Double.compare(o2.power, o1.power);
            }
        }
    }
    public void update_map(Map<Mage, Integer> map)
    {
        if (apprentices != null) {
            int num_of_apprentices = 0;
            for (Mage apprentice : apprentices)
            {
                apprentice.update_map(map);
                num_of_apprentices += map.getOrDefault(apprentice, 0) + 1;
            }
            map.put(this, num_of_apprentices);
        }
        else
        {
            map.put(this, 0);
        }
    }
}
