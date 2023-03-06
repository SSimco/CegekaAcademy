import { Box, Button, Container, Grid } from "@mui/material";
import { Fragment, useEffect, useMemo, useState } from "react";
import { FundraiserService } from "../Services/FundraiserService";
import { Fundraiser } from "../Models/Fundraiser";
import { FundraiserDonationRequest } from "../Models/FundraiserDonationRequest";
import { Link } from "react-router-dom";
import { FundraiserCard } from "../Components/FundraiserCard";

export const Fundraisers = () => {
    const fundraiserSerice = useMemo(() => new FundraiserService("http://localhost:5009"), []);
    const [fundraisers, setFundraisers] = useState<Fundraiser[]>([]);
    useEffect(() => {
        const fetchData = async () => {
            const fundraiserData = await fundraiserSerice.getAll();
            setFundraisers(fundraiserData);
        }
        fetchData();
    }, [fundraiserSerice]);

    const handleDonate = async (donationRequest: FundraiserDonationRequest) => {
        return await fundraiserSerice.donateToFundraiser(donationRequest).then(() => "", () => "An error has occured while donating, try again later.");
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
                        fundraisers.map((fundraiser) => (
                            <Grid item key={fundraiser.id} xs={12} sm={6} md={4}>
                                <FundraiserCard
                                    fundraiser={fundraiser}
                                    handleDonate={handleDonate}
                                />
                            </Grid>
                        ))
                    }
                </Grid>
            </Container>
        </Fragment>
    );
}