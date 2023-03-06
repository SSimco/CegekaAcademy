import { Box, Button, Container, Grid } from "@mui/material";
import { Fragment, useEffect, useState } from "react";
import { PetCard } from "../Components/PetCard";
import { Pet } from "../Models/Pet";
import { PetService } from "../Services/PetService";
import { Link } from "react-router-dom";

export const Pets = () => {
    const [pets, setPets] = useState<Pet[]>([]);

    useEffect(() => {
        const petService = new PetService("http://localhost:5009");
        const fetchData = async () => {
            const petData = await petService.getAll();
            setPets(petData);
        }
        fetchData();
    },[]);

    const handleAdopt = (pet: Pet) => {
        console.log("Someone wants to adopt " + pet.name);
    }

    return (
        <Fragment>
            <Box>
                <Button>
                    <Link to="/">Go to the home page</Link>
                </Button>
            </Box>
            <Container>
                <Grid container spacing={4}>
                    {
                        pets.map((pet) => (
                            <Grid item key={pet.id} xs={12} sm={6} md={4}>
                                <PetCard pet={pet} handleAdopt={() => handleAdopt(pet)}></PetCard>
                            </Grid>
                        ))
                    }
                </Grid>
            </Container>
        </Fragment>
    );
}