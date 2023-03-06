import { Autocomplete, Button, Dialog, DialogTitle, Stack, TextField, Typography } from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import { PetService } from "../Services/PetService";
import { useState } from "react";
import { Pet } from "../Models/Pet";
import { Person } from "../Models/Person";
import dayjs, { Dayjs } from "dayjs";
import { validatePersonId, validatePersonName } from "../Validators/PersonValidators";
import { validatePetName, validatePetType, validatePetWeight } from "../Validators/PetValidators";
import { PetTypes } from "../Validators/PetValidatorsConstants";

export interface IRescuePetDialogProps {
    open: boolean;
    setOpen: (state: boolean) => void;
    onPetRescue: (pet: Pet) => void;
}

export const RescuePetDialog = (props: IRescuePetDialogProps) => {
    const [petName, setPetName] = useState("");
    const [imageUrl, setImageUrl] = useState("");
    const [type, setType] = useState("");
    const [description, setDescription] = useState("");
    const [petBirthdate, setPetBirthdate] = useState<Dayjs | null>(dayjs());
    const [weightInKg, setWeightInKg] = useState("");
    const [rescuerName, setRescuerName] = useState("");
    const [rescuerIdNumber, setRescuerId] = useState("");
    const [rescuerBirthdate, setRescuerBirthdate] = useState<Dayjs | null>(dayjs());
    const [nameError, setNameError] = useState("");
    const [idError, setIdError] = useState("");
    const [petNameError, setPetNameError] = useState("");
    const [typeError, setTypeError] = useState("");
    const [weightError, setWeightError] = useState("");

    const petService = new PetService("http://localhost:5009");
    const isRequestValid = () => {
        var isValid = true;
        const nameValidationMessage = validatePersonName(rescuerName);
        if (nameValidationMessage !== null) {
            isValid = false;
            setNameError(nameValidationMessage);
        }
        const idValidationMessage = validatePersonId(rescuerIdNumber);
        if (idValidationMessage !== null) {
            isValid = false;
            setIdError(idValidationMessage);
        }
        const petNameValidationMessage = validatePetName(petName);
        if (petNameValidationMessage !== null) {
            isValid = false;
            setPetNameError(petNameValidationMessage);
        }
        const petTypeValidationMessage = validatePetType(type);
        if (petTypeValidationMessage !== null) {
            isValid = false;
            setTypeError(petTypeValidationMessage);
        }
        const weightValidationMessage = validatePetWeight(weightInKg);
        if (weightValidationMessage !== null) {
            isValid = false;
            setWeightError(weightValidationMessage);
        }
        return isValid;
    };
    const onClose = () => {
        setPetName("");
        setImageUrl("");
        setType("");
        setDescription("");
        setPetBirthdate(dayjs());
        setWeightInKg("");
        setRescuerName("");
        setRescuerId("");
        setRescuerBirthdate(dayjs());
        setNameError("");
        setIdError("");
        setPetNameError("");
        setTypeError("");
        setWeightError("");
        props.setOpen(false);
    };
    const onRescueClick = () => {
        if (!isRequestValid()) {
            return;
        }
        const pet = new Pet(
            petName,
            imageUrl,
            type,
            description,
            petBirthdate === null ? new Date() : petBirthdate.toDate(),
            Number(weightInKg),
            new Person(rescuerName, Number(rescuerIdNumber), rescuerBirthdate?.toDate())
        );
        petService.rescue(pet).then(() => {
            props.setOpen(false);
            props.onPetRescue(pet);
        },
            () => {

            });

    };

    return (
        <Dialog open={props.open} onClose={onClose} >
            <DialogTitle>Rescue a pet</DialogTitle>
            <Stack
                component="form"
                justifyContent="space-evenly"
                spacing={5}
                noValidate
                autoComplete="off"
                margin={5}
            >
                <Typography variant="h6" color="text.primary">
                    Rescuer information
                </Typography>
                <TextField
                    label="Full name"
                    variant="outlined"
                    onChange={() => {
                        if (nameError !== "") {
                            setNameError("");
                        }
                    }}
                    onBlur={event => {
                        setRescuerName(event.target.value)
                    }}
                    error={nameError !== ""}
                    helperText={nameError}
                />
                <TextField
                    label="ID number"
                    variant="outlined"
                    onChange={() => {
                        if (idError !== "") {
                            setIdError("");
                        }
                    }}
                    onBlur={event => {
                        setRescuerId(event.target.value);
                    }}
                    error={idError !== ""}
                    helperText={idError}
                />
                <DatePicker label="Birthdate" value={rescuerBirthdate} onChange={(date) => setRescuerBirthdate(date)} />
                <Typography variant="h6" color="text.primary">
                    Pet information
                </Typography>
                <TextField
                    label="Pet name"
                    variant="outlined"
                    onChange={() => {
                        if (nameError !== "") {
                            setPetNameError("");
                        }
                    }}
                    onBlur={event => {
                        setPetName(event.target.value)
                    }}
                    error={petNameError !== ""}
                    helperText={petNameError}
                />
                <TextField
                    label="Image URL"
                    variant="outlined"
                    onBlur={event => {
                        setImageUrl(event.target.value)
                    }}
                />
                <Autocomplete
                    disablePortal
                    options={PetTypes}
                    renderInput={(params) =>
                        <TextField
                            {...params}
                            label="Type"
                            onChange={() => {
                                if (typeError !== "") {
                                    setTypeError("");
                                }
                            }}
                            onBlur={event => {
                                setType(event.target.value)
                            }}
                            error={typeError !== ""}
                            helperText={typeError}
                        />}
                />

                <TextField
                    label="Description"
                    variant="outlined"
                    onBlur={event => {
                        setDescription(event.target.value)
                    }}
                />
                <DatePicker label="Pet birthdate" value={rescuerBirthdate} onChange={(date) => setPetBirthdate(date)} />
                <TextField
                    label="Weight"
                    variant="outlined"
                    onChange={() => {
                        if (weightError !== "") {
                            setWeightError("");
                        }
                    }}
                    onBlur={event => {
                        setWeightInKg(event.target.value)
                    }}
                    error={weightError !== ""}
                    helperText={weightError}
                />
                <Button variant="contained" onClick={onRescueClick}>Rescue</Button>
            </Stack>
        </Dialog>);
}