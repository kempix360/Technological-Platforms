package org.example;

import jakarta.persistence.*;

import java.util.List;

@Entity
public class Tower {
    @Id
    private String name;
    private int height;
    @OneToMany
    private List<Mage> mages;

    public Tower(){
    }

    public Tower(String name, int height, List<Mage> mages){
        this.name = name;
        this.height = height;
        this.mages = mages;
    }

    public String getName() {
        return name;
    }

    public int getHeight() {
        return height;
    }

    public void setHeight(int height) {
        this.height = height;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Mage> getMages() {
        return mages;
    }

    public void setMages(List<Mage> mages) {
        this.mages = mages;
    }

    public void addMage(Mage mage) {
        mages.add(mage);
    }

    public void removeMage(Mage mage) {
        mages.remove(mage);
    }
}
