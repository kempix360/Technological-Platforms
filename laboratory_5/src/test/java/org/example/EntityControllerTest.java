package org.example;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;

import java.util.ArrayList;
import static org.junit.jupiter.api.Assertions.*;

public class EntityControllerTest {
    @Mock
    private MageRepository repository;

    private MageController controller;

    @BeforeEach
    public void setup() {
        repository = new MageRepository(new ArrayList<>());
        controller = new MageController(repository);
    }

    @Test
    public void SaveNonExistingEntity() {
        String result = controller.save(new Mage("Harry Potter", 100));
        assertEquals(result, "done");
    }

    @Test
    public void SaveAlreadyExistingEntity() {
        controller.save(new Mage("Harry Potter", 100));
        String result = controller.save(new Mage("Harry Potter", 100));
        assertEquals(result, "bad request");
    }

    @Test
    public void FindExistingEntity() {
        Mage mage = new Mage("Harry Potter", 100);
        controller.save(mage);
        String result = controller.find("Harry Potter");
        assertEquals(result, mage.toString());
    }

    @Test
    public void FindNonExistingEntity() {
        String result = controller.find("Hermiona Granger");
        assertEquals(result, "not found");
    }

    @Test
    public void DeleteExistingEntity() {
        controller.save(new Mage("Harry Potter", 100));
        String result = controller.delete("Harry Potter");
        assertEquals(result, "done");
    }

    @Test
    public void DeleteNonExistingEntity() {
        String result = controller.delete("Ron Weasley");
        assertEquals(result, "not found");
    }
}
