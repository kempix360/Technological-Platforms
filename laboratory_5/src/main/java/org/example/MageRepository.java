package org.example;

import java.util.Collection;
import java.util.Optional;

public class MageRepository {
    private final Collection<Mage> collection;

    public MageRepository(Collection<Mage> collection) {
        this.collection = collection;
    }

    public Optional<Mage> find(String name) {
        return collection.stream()
            .filter(mage -> mage.getName().equals(name))
            .findFirst();
    }
    public void delete(String name) {
        if (this.find(name).isEmpty())
            throw new IllegalArgumentException("Mage doesn't exist.");
        else
            collection.removeIf(mage -> mage.getName().equals(name));
    }
    public void save(Mage mage) {
        if (this.find(mage.getName()).isPresent())
            throw new IllegalArgumentException("Mage already exists.");
        else
            collection.add(mage);
    }
}