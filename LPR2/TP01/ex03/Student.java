package EX03;

public class Student extends Person {
    private String program;
    private int year;
    private double fee;

    public Student(String name, String address, String program, int year, double fee) {
        super(name, address);
        setProgram(program);
        setYear(year);
        setFee(fee);
    }

    public String getProgram() {
        return program;
    }

    public void setProgram(String program) {
        if (program == null || program.trim().isEmpty()) {
            throw new IllegalArgumentException("Programa não pode ser vazio");
        }
        this.program = program.trim();
    }

    public int getYear() {
        return year;
    }

    public void setYear(int year) {
        if (year < 1) {
            throw new IllegalArgumentException("Ano deve ser maior ou igual a 1");
        }
        this.year = year;
    }

    public double getFee() {
        return fee;
    }

    public void setFee(double fee) {
        if (fee < 0) {
            throw new IllegalArgumentException("Mensalidade não pode ser negativa");
        }
        this.fee = fee;
    }

    @Override
    public String toString() {
        return "Student[" + super.toString() +
                ",program=" + program +
                ",year=" + year +
                ",fee=" + fee + "]";
    }
}