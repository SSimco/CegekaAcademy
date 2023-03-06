import { Container, Typography, Grid, Button, Box, Dialog, DialogTitle, Stack } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { RescuePetDialog } from "../Components/RescuePetDialog";
import { Pet } from "../Models/Pet";

export const Home = () => {

    const navigate = useNavigate();
    const RedirectToPets = () => {
        console.log("I wanna redirect to pets");
        navigate(`/Pets`);
    }
    const [openRescue, setOpenRescue] = useState(false);
    const [rescuedPet, setRescuedPet] = useState<Pet | null>(null);
    const [openRescueSuccessful, setOpenRescueSuccessful] = useState(false);
    const onPetRescue = (pet: Pet) => {
        setRescuedPet(pet);
        setOpenRescueSuccessful(true);
    }
    const RescueSuccessfulDialogContent = () => {
        if (rescuedPet != null) {
            return (
                <>
                    <DialogTitle>Thank you for rescuing {rescuedPet.name}!</DialogTitle>
                    <Stack
                        alignItems="center"
                        spacing={2}
                        margin={5}
                    >
                        <Typography gutterBottom variant="h6" >
                            {rescuedPet.name} can be brought in our shelter on any day between 8:30AM and 3:30PM.
                        </Typography>
                        <Button variant="contained" onClick={() => setOpenRescueSuccessful(false)}>OK</Button>
                    </Stack>
                </>
            );
        }
        return <></>;
    }
    return (
        <Container maxWidth="lg" sx={{ marginTop: "3rem" }}>
            <Typography gutterBottom variant="h2" component="div">
                Pet Shelter
            </Typography>
            <Grid container spacing={5} sx={{ justifyContent: "center", marginTop: "5rem", height: "10rem" }}>
                <Grid item xs={6}>
                    <Button variant="contained"
                        color="primary"
                        sx={{ width: "100%", height: "100%" }} onClick={RedirectToPets}>
                        Pets
                    </Button>
                </Grid>
                <Grid item xs={6}>
                    <Button variant="contained"
                        color="secondary"
                        sx={{ width: "100%", height: "100%", }}
                        onClick={() => navigate("/Fundraisers")}>
                        Fundraisers
                    </Button>
                </Grid>
            </Grid>
            <Box sx={{ justifyContent: "center", marginTop: "5rem" }}>
                <Typography variant='body1'>
                    Found a stray pet on the street but cannot adopt it?
                </Typography>
                <Button variant="contained" color="success" onClick={() => setOpenRescue(true)}>
                    Rescue it!
                </Button>
                <RescuePetDialog open={openRescue} setOpen={setOpenRescue} onPetRescue={onPetRescue} />
                <Dialog open={openRescueSuccessful} onClose={() => setOpenRescueSuccessful(false)} >
                    <RescueSuccessfulDialogContent />
                </Dialog>
            </Box>
        </Container>
    );
}