package EX03;

public class Staff extends Person {
    private String school;
    private double pay;

    public Staff(String name, String address, String school, double pay) {
        super(name, address);
        setSchool(school);
        setPay(pay);
    }

    public String getSchool() {
        return school;
    }

    public void setSchool(String school) {
        if (school == null || school.trim().isEmpty()) {
            throw new IllegalArgumentException("Escola não pode ser vazia");
        }
        this.school = school.trim();
    }

    public double getPay() {
        return pay;
    }

    public void setPay(double pay) {
        if (pay < 0) {
            throw new IllegalArgumentException("Salário não pode ser negativo");
        }
        this.pay = pay;
    }

    @Override
    public String toString() {
        return "Staff[" + super.toString() +
                ",school=" + school +
                ",pay=" + pay + "]";
    }
}