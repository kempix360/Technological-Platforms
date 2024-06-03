package org.example;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.util.ArrayList;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.when;

public class EntityRepositoryTest {
    @Mock
    private MageRepository repository;

    @BeforeEach
    public void setup() {
        repository = new MageRepository(new ArrayList<>());
    }

    @Test
    public void SaveNonExistingEntity() {
        Mage mage = new Mage("Harry Potter", 100);
        repository.save(mage);

        Optional<Mage> result = repository.find("Harry Potter");
        assertTrue(result.isPresent());
        assertEquals(mage, result.get());
    }

    @Test
    public void SaveAlreadyExistingEntity() {
        Mage mage = new Mage("Harry Potter", 100);
        repository.save(mage);

        Optional<Mage> result = repository.find("Harry Potter");
        assertTrue(result.isPresent());
        assertEquals(mage, result.get());
    }

    @Test
    public void FindExistingEntity() {
        Mage mage = new Mage("Harry Potter", 100);
        repository.save(mage);
        Optional<Mage> result = repository.find("Harry Potter");
        assertTrue(result.isPresent());
        assertEquals("Harry Potter", result.get().getName());
    }

    @Test
    public void FindNonExistingEntity() {
        Optional<Mage> result = repository.find("Hermiona Granger");
        assertFalse(result.isPresent());
    }

    @Test
    public void DeleteExistingEntity() {
        Mage mage = new Mage("Ron Weasley", 80);
        repository.save(mage);
        repository.delete(mage.getName());

        Optional<Mage> result = repository.find("Ron Weasley");
        assertFalse(result.isPresent());
    }

    @Test
    public void DeleteNonExistingEntity() {
        assertThrows(IllegalArgumentException.class,()->repository.delete("Ron Weasley"));
    }
}
