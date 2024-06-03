package org.example;

import java.util.Optional;

public class MageController {
    private final MageRepository repository;

    public MageController(MageRepository repository) {
        this.repository = repository;
    }

    public String find(String name) {
        Optional<Mage> mage = repository.find(name);
        if (mage.isPresent()) {
            return mage.get().toString();
        } else {
            return "not found";
        }
    }
    public String delete(String name) {
        try {
            repository.delete(name);
            return "done";
        } catch (IllegalArgumentException e) {
            return "not found";
        }
    }
    public String save(Mage mage) {
        try {
            repository.save(mage);
            return "done";
        } catch (IllegalArgumentException e) {
            return "bad request";
        }
    }
}